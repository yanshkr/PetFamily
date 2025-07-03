using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PetFamily.Accounts.Application;
using PetFamily.Accounts.Domain.DataModels;
using PetFamily.Accounts.Infrastructure.Options;

namespace PetFamily.Accounts.Infrastructure.Providers;

public class JwtTokenProvider(IOptions<JwtOptions> jwtOptions) : ITokenProvider
{
    public string GenerateAccessToken(User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Value.Key));
        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        Claim[] claims =
        [
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new(JwtRegisteredClaimNames.Email, user.Email ?? "")
        ];

        var jwtToken = new JwtSecurityToken(
            issuer: jwtOptions.Value.Issuer,
            audience: jwtOptions.Value.Audience,
            expires: DateTime.UtcNow.AddMinutes(jwtOptions.Value.ExpiredMinutesTime),
            signingCredentials: signingCredentials,
            claims: claims);

        var stringToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);

        return stringToken;
    }
}