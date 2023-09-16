using MerchantAPI.Configs;
using MerchantAPI.Models;
using MerchantAPI.Services;
using MongoDB.Driver;
namespace MerchantAPI.Repositories;

public class AuthRepository:IAuthRepository
{
    private IMongoCollection<UserDatabaseModel> _collection { get; set; }
    // private readonly MongoDBSettings _mongoDbSettings;
    
    public AuthRepository(MongoDbSettings mongoDbSettings)
    {
        // _mongoDbSettings = mongoDbSettings;
        var client = new MongoClient(mongoDbSettings.ConnectionString);
        var db = client.GetDatabase(mongoDbSettings.DbName);
        var collection = db.GetCollection<UserDatabaseModel>(mongoDbSettings.UserCollectionName);
        _collection = collection;
    }
    public async Task<UserDatabaseModel> CreateMerchant(UserDatabaseModel userDatabaseModel)
    {
        await _collection.InsertOneAsync(userDatabaseModel);
        return userDatabaseModel;
    }
    public async Task<UserDatabaseModel> FindByUsername(string username)
    {
        var userRecord = await _collection.FindAsync(i => i.Username == username);
        var user = await userRecord?.FirstOrDefaultAsync();  
        return user;
    }
}
