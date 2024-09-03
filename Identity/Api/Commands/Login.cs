using Identity.Infrastructure.Models;

namespace Identity.Core.Commands;

using MediatR;

public class Login : IRequest<TokensPair?>
{
    public string Username { get; set; }

    public string Password { get; set; }
}