using MerchantAPI.Models;
using MerchantAPI.V1.Models.RequestModels;

namespace MerchantAPI.Extensions.Mappers;

public class MerchantMappers
{
    public static Task<Merchant> Create(MerchantCreateRequestModel request)
    {
        Merchant merchant = new Merchant(request.Name, request.CityCode, request.City, request.Neighborhood,
            DateTime.UtcNow, DateTime.UtcNow);
        return Task.FromResult(merchant);
    }

    public static Task<Merchant> Update(Merchant merchant, MerchantUpdateRequestModel request)
    {
        Merchant newMerchant = new Merchant(request.Name, request.CityCode, request.City, request.Neighborhood,
            DateTime.UtcNow, DateTime.UtcNow);
        newMerchant.Id = merchant.Id;
        newMerchant.CreatedAt = merchant.CreatedAt;
        return Task.FromResult(newMerchant);
    }
}