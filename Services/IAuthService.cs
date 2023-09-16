using MerchantAPI.Models;
using MerchantAPI.V1.Models.RequestModels;

namespace MerchantAPI.Services;

public interface IAuthService
{
    public Task<UserDatabaseModel> CreateUser(SignupRequestModel requestModel);
    public Task<UserDatabaseModel> FindByUsername(string username);
    public Task<bool> SecurityCheck(LoginRequestModel loginRequestModel);
}