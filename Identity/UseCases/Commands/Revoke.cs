namespace Identity.Api.Commands;

using MediatR;

public class Revoke : IRequest
{
    public string Username { get; set; }
}
