using MerchantAPI.Filters;
using MerchantAPI.Models;
using MerchantAPI.Models.Errors;
using MerchantAPI.Repositories;
using MerchantAPI.V1.Models.RequestModels;
using MongoDB.Driver;

namespace MerchantAPI.Services;

public class Service:IService
{
    private readonly IRepository _repository;
    private readonly ILogger<Service> _logger;
    
    public Service(IRepository repository, ILogger<Service> logger)
    {
        _repository = repository;
        _logger = logger;
    }
    public async Task<Merchant> GetMerchant(string id)
    {
        var merchant = await _repository.GetMerchant(id);
        if (merchant == null)
        {
            throw new MerchantNotFound(id);
        }
        return merchant;
    }
    public async Task<Merchant> CreateMerchant(Merchant merchant)
    {
        var newMerchant = await _repository.CreateMerchant(merchant);
        return newMerchant; 
    }
    public async Task<List<Merchant>> GetAllMerchants(FilterParams filterParams)
    {
        var filter = Builders<Merchant>.Filter.Empty;

        if (filterParams.City != null)
        {
            filter &= Builders<Merchant>.Filter.Eq(x => x.Address.City, filterParams.City);
        }
        if (filterParams.MinValue != null)
        {
            filter &= Builders<Merchant>.Filter.Gt(x => x.Address.CityCode, filterParams.MinValue);
        }
        if (filterParams.MaxValue != null)
        {
            filter &= Builders<Merchant>.Filter.Lt(x => x.Address.CityCode, filterParams.MaxValue);
        }

        filter = filterParams.SearchTerm switch
        {
            "name" when filterParams.Search != null => filter &
                                                       Builders<Merchant>.Filter.Where(x =>
                                                           x.Name.Contains(filterParams.Search)),
            "city" when filterParams.Search != null => filter &
                                                       Builders<Merchant>.Filter.Where(x =>
                                                           x.Address.City.Contains(filterParams.Search)),
            "neighborhood" when filterParams.Search != null => filter &
                                                               Builders<Merchant>.Filter.Where(x =>
                                                                   x.Address.Neighborhood
                                                                       .Contains(filterParams.Search)),
            null when filterParams.Search == null => filter,
            _ => throw new ImproperSearch(filterParams.SearchTerm,filterParams.Search)
        };

        List<Merchant> merchants = await _repository.GetAll(filter, filterParams);
        return merchants;
    }
    public async Task DeleteMerchant(string id)
    {
        var merchant = await _repository.GetMerchant(id);
        if (merchant == null)
        {
            throw new MerchantNotFound(id);
        }
        await _repository.DeleteMerchant(id);
    }
    public async Task UpdateMerchant(string id, Merchant merchant)
    {
        await _repository.UpdateMerchant(id, merchant);
    }
    public async Task UpdateAddressOfMerchant(string id, MerchantAddressUpdateRequestModel request)
    {
        var merchant = await _repository.GetMerchant(id);
        if (merchant == null)
        {
            throw new MerchantNotFound(id);
        }
        await _repository.UpdateAddressOfMerchant(id, request);
    }
}