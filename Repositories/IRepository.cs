using MerchantAPI.Filters;
using MerchantAPI.Models;
using MerchantAPI.V1.Models.RequestModels;
using MongoDB.Driver;

namespace MerchantAPI.Repositories;

public interface IRepository
{
    public Task<Merchant> GetMerchant(string id);
    public Task<Merchant> CreateMerchant(Merchant merchant);
    public Task<List<Merchant>> GetAll(FilterDefinition<Merchant> filter, FilterParams filterParams);
    public Task DeleteMerchant(string id);
    public Task UpdateMerchant(string id, Merchant merchant);
    public Task UpdateAddressOfMerchant(string id, MerchantAddressUpdateRequestModel request);
}