namespace MerchantAPI.Models.Errors;

public class NotFound : ErrorDetails
{
    public NotFound(string message)
    {
        StatusCode = 404;
        Message = message;
    }
}

public class MerchantNotFound : NotFound
{ 
    public MerchantNotFound(string id) : base(message: $"Given merchant not found:{id}."){}
}

