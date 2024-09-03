namespace Identity.Api.Commands;

using MediatR;

public class Promote : IRequest
{
    public string Username { get; set; }
    public IEnumerable<string> Roles { get; set; }
}