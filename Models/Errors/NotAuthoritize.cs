namespace MerchantAPI.Models.Errors;

public class NotAuthorize:ErrorDetails
{
    public NotAuthorize()
    {
        StatusCode = 401;
        Message = "Not Authorize";
    }
}
