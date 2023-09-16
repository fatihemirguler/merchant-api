using MerchantAPI.Extensions.Auth;
using MerchantAPI.Models;
using MerchantAPI.V1.Models.RequestModels;

namespace MerchantAPI.Extensions.Mappers;

public class UserMappers
{
    private readonly JwtTokenGenerator _tokenGenerator;

    public UserMappers(JwtTokenGenerator tokenGenerator)
    {
        _tokenGenerator = tokenGenerator;
    }

    public Task<UserDatabaseModel> Create(SignupRequestModel requestModel)
    {
        string username = requestModel.Username;
        string pass = requestModel.Password;
        var token = _tokenGenerator.GenerateToken(username, pass);
        UserDatabaseModel userDatabaseModel = new UserDatabaseModel(username, token);
        return Task.FromResult(userDatabaseModel);
    }
}