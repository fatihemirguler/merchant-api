using MerchantAPI.Models;

namespace MerchantAPI.Services;

public interface IAuthRepository
{
    public Task<UserDatabaseModel> CreateMerchant(UserDatabaseModel userDatabaseModel);
    public Task<UserDatabaseModel> FindByUsername(string username);
}