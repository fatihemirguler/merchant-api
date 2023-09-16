using MerchantAPI.Configs;
using MerchantAPI.Filters;
using MerchantAPI.Models;
using MerchantAPI.V1.Models.RequestModels;
using MongoDB.Driver;


namespace MerchantAPI.Repositories;

public class Repository : IRepository
{
    private IMongoCollection<Merchant> _collection { get; set; }
    // private readonly MongoDBSettings _mongoDbSettings;
    public Repository(MongoDbSettings mongoDbSettings)
    {
        // _mongoDbSettings = mongoDbSettings;
        var client = new MongoClient(mongoDbSettings.ConnectionString);
        var db = client.GetDatabase(mongoDbSettings.DbName);
        var collection = db.GetCollection<Merchant>(mongoDbSettings.MerchantCollectionName);
        _collection = collection;
    }
    public async Task<Merchant> GetMerchant(string id)
    {
        var merchantRecord = await _collection.FindAsync(i => i.Id == id);
        var merchant = await merchantRecord.FirstOrDefaultAsync();  
        return merchant;
    }
    public async Task<Merchant> CreateMerchant(Merchant merchant)
    {
        await _collection.InsertOneAsync(merchant);
        return merchant;
    }
    public async Task<List<Merchant>> GetAll(FilterDefinition<Merchant> filter, FilterParams filterParams)
    {
        var merchants = await _collection.Find(filter)
            .SortBy(x=>x.CreatedAt)
            .Limit(filterParams.Limit).Skip(filterParams.Offset)
            .ToListAsync();
        
        return merchants;
    }
    public async Task DeleteMerchant(string id)
    {
        await _collection.DeleteOneAsync(i => i.Id == id);
    }
    public async Task UpdateMerchant(string id, Merchant merchant)
    {
        await _collection.ReplaceOneAsync(i => i.Id == id, merchant);
    }
    public async Task UpdateAddressOfMerchant(string id, MerchantAddressUpdateRequestModel request)
    {
        var filter = Builders<Merchant>.Filter.Eq(i => i.Id, id);
        var update = Builders<Merchant>.Update.Set(merchant => merchant.Address.CityCode, request.CityCode)
            .Set(merchant => merchant.Address.City, request.City)
            .Set(merchant => merchant.Address.Neighborhood, request.Neighborhood);
        await _collection.UpdateOneAsync(filter, update);
    }
}







// var update = Builders<Merchant>.Update.Set(merchant.Address.City, request.City)
//     .Set(merchant.Address.Neighborhood, request.Neighborhood)
//     .Set(merchant.Address.CityCode.ToString(), request.CityCode); // why no int,int?