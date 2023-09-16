using MerchantAPI.Extensions;
using MerchantAPI.Extensions.Auth;
using MerchantAPI.Extensions.Mappers;
using MerchantAPI.Models;
using MerchantAPI.V1.Models.RequestModels;

namespace MerchantAPI.Services;

public class AuthService:IAuthService
{
    private readonly IAuthRepository _authRepository;
    private readonly UserMappers _userMapper;

    public AuthService (IAuthRepository authRepository,UserMappers userMapper)
    {
        _authRepository = authRepository;
        _userMapper = userMapper;
    }
    public async Task<UserDatabaseModel> CreateUser(SignupRequestModel requestModel)
    {
        var user = await _authRepository.FindByUsername(requestModel.Username);
        if (user != null)
        {
            throw new Exception(); //username occupied
        }
        var userDatabaseModel = await _userMapper.Create(requestModel);
        var newMerchant = await _authRepository.CreateMerchant(userDatabaseModel);
        return newMerchant; 
    }
    
    public async Task<UserDatabaseModel> FindByUsername(string username)
    {
        var user = await _authRepository.FindByUsername(username);
        if (user == null)
        {
            throw new Exception(); //username not found
        }
        return user;
    }

    public async Task<bool> SecurityCheck(LoginRequestModel loginRequestModel)
    {
        string loginPass = loginRequestModel.Password;
        var userDatabaseModel = await _authRepository.FindByUsername(loginRequestModel.Username);
        var token = userDatabaseModel.AuthToken;
        var pass= await JwtTokenDecoder.DecodeToken(token);
        
        return loginPass == pass;
    }
}