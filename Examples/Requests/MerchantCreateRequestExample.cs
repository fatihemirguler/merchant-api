using MerchantAPI.V1.Models.RequestModels;
using Swashbuckle.AspNetCore.Filters;

namespace MerchantAPI.Examples.Requests;

public class MerchantCreateRequestExample :IExamplesProvider<MerchantCreateRequestModel>
{
    public MerchantCreateRequestModel GetExamples()
    {
        return new MerchantCreateRequestModel(name: "slim shady", cityCode: 12, city: "it'd be good if you enter a city not from guetamala",
            neighborhood: "enter a neighborhood that it's character length is not longer than 10, like detroit");
    }
}