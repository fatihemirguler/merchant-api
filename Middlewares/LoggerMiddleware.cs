using MerchantAPI.Models.Errors;
using MerchantAPI.Models.Loggers;
using Microsoft.AspNetCore.Http.HttpResults;

namespace MerchantAPI.Middlewares;

public class LoggerMiddleware
{
    private readonly RequestDelegate _next;
    private ILogger<LoggerMiddleware> _logger;

    public LoggerMiddleware(RequestDelegate next, ILogger<LoggerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext) 
    {
        try
        {
            await _next(httpContext);
            
            var log = new LoggingDetails(httpContext.Response.StatusCode, $"{httpContext.Response}", httpContext.Request.Path).ToString();
            
            if (httpContext.Response.StatusCode is >= 200 and < 400)
            {
                var okLog = new LoggingDetails(httpContext.Response.StatusCode, $"Ok", httpContext.Request.Path).ToString();
                _logger.LogInformation(okLog);
            }
            else if (httpContext.Response.StatusCode is >= 400 and < 500)
            {
                _logger.LogWarning(log);
                throw new ErrorBetween400And500(httpContext);
            }
            else if (httpContext.Response.StatusCode >= 500)
            {
                _logger.LogError(log);
                throw new Exception();
            }
        }
        catch (ErrorDetails err)
        {
            var log = new LoggingDetails(err.StatusCode, err.Message, httpContext.Request.Path).ToString();
            
            if (err.StatusCode > 400 && err.StatusCode < 500)
            {
                _logger.LogWarning(log);
                
                throw;
            }
            
            _logger.LogError(log);
            
            throw;
        }
        catch (Exception ex)
        {
            var log = new LoggingDetails(httpContext.Response.StatusCode, ex.Message, httpContext.Request.Path).ToString();

            _logger.LogError(log);

            throw;
        }
    }
}