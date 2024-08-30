using MediatR;

namespace Identity.Core.Commands;

public class Revoke : IRequest
{
    public string Username { get; set; }
}
