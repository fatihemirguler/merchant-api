using Amazon.Runtime.Internal.Auth;
using MerchantAPI.Extensions;
using MerchantAPI.Extensions.Auth;
using MerchantAPI.Extensions.Mappers;
using MerchantAPI.Filters;
using MerchantAPI.Models.Errors;
using MerchantAPI.Services;
using MerchantAPI.V1.Models.RequestModels;
using MerchantAPI.V1.Models.ResponseModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace MerchantAPI.V1.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]

public class AuthController: ControllerBase
{
    private readonly IAuthService _authService;
    private readonly JwtTokenGenerator _tokenGenerator;

    public AuthController(JwtTokenGenerator tokenGenerator, IAuthService authService)
    {
        _tokenGenerator = tokenGenerator;
        _authService = authService;
    }
    
    [HttpPost("signup")]
    public async Task<IActionResult> Signup([FromBody] SignupRequestModel requestModel)
    {
        var newUser = await _authService.CreateUser(requestModel);
        return Created(HttpContext.Request.Path, newUser);
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestModel requestModel)
    {
        var securityCheckResult = await _authService.SecurityCheck(requestModel);
        
        if (securityCheckResult)
        {
            return Ok(); //Add authorization
        }
        
        throw new NotAuthorize();
    }
}