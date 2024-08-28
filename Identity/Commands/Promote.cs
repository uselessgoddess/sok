using FluentValidation;
using Identity.Models;
using MediatR;

namespace Identity.Commands;

public class Promote : IRequest
{
    public string Id { get; set; }
    public IEnumerable<Role> Roles { get; set; }
}