using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Identity.Models;
using Microsoft.IdentityModel.Tokens;

namespace Identity.Services;

public class TokenService(IConfiguration config)
{
    public string Access(AppUser user, IEnumerable<Role> roles)
    {
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.UserName),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(ClaimTypes.NameIdentifier, user.Id)
        };
        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role.ToString())));

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