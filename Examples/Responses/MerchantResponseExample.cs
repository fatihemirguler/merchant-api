using MerchantAPI.Models;
using MerchantAPI.V1.Models.RequestModels;
using MerchantAPI.V1.Models.ResponseModels;
using Swashbuckle.AspNetCore.Filters;

namespace MerchantAPI.Examples.Responses;

public class MerchantResponseModelExample :IExamplesProvider<MerchantResponseModel>
{
    public MerchantResponseModel GetExamples()
    {
        Merchant merchant = new Merchant("Biladerler Pide Firin", 34, "Istanbul",
            "Uskudar", DateTime.Parse("1453-05-29T17:45:25.177+00:00"),
            DateTime.Parse("1923-10-29T17:45:25.177+00:00"));
        return new MerchantResponseModel(merchant);
    }
}