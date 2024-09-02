namespace Identity.Core.Commands;

using MediatR;

public class Revoke : IRequest
{
    public string Username { get; set; }
}
