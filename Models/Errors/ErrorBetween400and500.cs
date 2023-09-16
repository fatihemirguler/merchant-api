namespace MerchantAPI.Models.Errors;

public class ErrorBetween400And500:ErrorDetails
{
    public ErrorBetween400And500(HttpContext httpContext)
    {
        StatusCode = httpContext.Response.StatusCode;
        Message = "Warning";
        if (StatusCode==401)
        {
            StatusCode = 401;
            Message = "Not Authorize";
        }
    }
}