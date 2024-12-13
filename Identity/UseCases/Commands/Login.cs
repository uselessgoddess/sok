namespace Identity.Api.Commands;

using Identity.Core.Models;
using MediatR;

public class Login : IRequest<TokensPair?>
{
    public string Username { get; set; }

    public string Password { get; set; }
}