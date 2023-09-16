namespace MerchantAPI.Extensions.Auth;

using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

public class JwtTokenDecoder
{
    public static Task<string> DecodeToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var jwtToken = tokenHandler.ReadJwtToken(token);
        
        var uniqueNameClaim = jwtToken.Claims.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.UniqueName)!;
        string password = uniqueNameClaim.Value;
        return Task.FromResult(password);
    }
}
