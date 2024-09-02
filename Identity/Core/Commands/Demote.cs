using Identity.Infrastructure.Models;

namespace Identity.Core.Commands;

using MediatR;

public class Demote : IRequest
{
    public string Id { get; set; }
    public IEnumerable<Role> Roles { get; set; }
}