using MerchantAPI.Models.Errors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MerchantAPI.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var error = new ErrorDetails()
            {
                StatusCode = 419,
                Message = context.Exception.Message
            };
            
            throw error;
            
        }
    }
    
}