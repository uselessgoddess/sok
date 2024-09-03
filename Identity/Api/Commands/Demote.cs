namespace Identity.Api.Commands;

using MediatR;

public class Demote : IRequest
{
    public string Id { get; set; }
    public IEnumerable<string> Roles { get; set; }
}