namespace Identity.Api.Commands;

using MediatR;

public class Register : IRequest
{
    public string Username { get; set; }

    public string Password { get; set; }

    public string Email { get; set; }
}