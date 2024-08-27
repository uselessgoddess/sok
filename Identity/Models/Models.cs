using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Identity.Models;

public class AppUser : IdentityUser
{
    public DateTime CreatedDate { get; set; }
}

public class RefreshToken
{
    public int Id { get; set; }
    public string Token { get; set; }
    public string UserId { get; set; }
    public AppUser User { get; set; }
    public DateTime Expire { get; set; }
    public bool IsRevoked { get; set; }
}

public class TokensPair
{
    public string Access { get; set; }
    public string Refresh { get; set; }
}
