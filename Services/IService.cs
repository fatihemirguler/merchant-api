using MerchantAPI.Filters;
using MerchantAPI.Models;
using MerchantAPI.V1.Models.RequestModels;

namespace MerchantAPI.Services;

public interface IService
{
    public Task<Merchant> GetMerchant(string id);
    public Task<Merchant> CreateMerchant(Merchant merchant);
    public Task<List<Merchant>> GetAllMerchants(FilterParams filterParams);
    public Task DeleteMerchant(string id);
    public Task UpdateMerchant(string id, Merchant merchant);
    public Task UpdateAddressOfMerchant(string id, MerchantAddressUpdateRequestModel addressUpdateRequestModel);
}