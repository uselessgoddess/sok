namespace Identity.Infrastructure.Models;

public class RefreshToken
{
    public int Id { get; set; }

    public string Token { get; set; }

    public string UserId { get; set; }

    public AppUser User { get; set; }

    public DateTime Expire { get; set; }

    public bool IsRevoked { get; set; }
}