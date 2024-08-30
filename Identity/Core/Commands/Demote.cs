namespace Identity.Core.Commands;

using Identity.Core.Models;
using MediatR;

public class Demote : IRequest
{
    public string Id { get; set; }
    public IEnumerable<Role> Roles { get; set; }
}