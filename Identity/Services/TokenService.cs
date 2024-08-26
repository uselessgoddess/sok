using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Identity.Models;
using Microsoft.IdentityModel.Tokens;

namespace Identity.Services;

public class TokenService(IConfiguration config)
{
    public string Access(AppUser user)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.NameIdentifier, user.Id)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expires = DateTime.Now.AddMinutes(Convert.ToDouble(config["Jwt:AccessTokenExpiryMinutes"]));

        var token = new JwtSecurityToken(
            config["Jwt:Issuer"],
            config["Jwt:Audience"],
            claims,
            expires,
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public RefreshToken Refresh(string userId)
    {
        return new RefreshToken
        {
            Token = Guid.NewGuid().ToString(),
            UserId = userId,
            Expire = DateTime.Now.AddDays(Convert.ToDouble(config["Jwt:RefreshExpireDays"]))
        };
    }
}