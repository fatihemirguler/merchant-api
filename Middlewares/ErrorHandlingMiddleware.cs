using System.Net;
using MerchantAPI.Models.Errors;

namespace MerchantAPI.Middlewares;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (ErrorDetails ex)
        {
            await HandleErrorAsync(httpContext, ex);
        }
        catch (Exception)
        {
            await HandleExceptionAsync(httpContext);
        }
    }
    private async Task HandleErrorAsync(HttpContext context, ErrorDetails exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = exception.StatusCode;

        await context.Response.WriteAsync(new ErrorDetails
        {
            StatusCode = exception.StatusCode,
            Message = exception.Message
        }.ToString());
    }
    private async Task HandleExceptionAsync(HttpContext context)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        await context.Response.WriteAsync(new ErrorDetails()
        {
            StatusCode = context.Response.StatusCode,
            Message = "Internal Server Error from the custom middleware."
        }.ToString());
    }
}

