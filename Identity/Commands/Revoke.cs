using Identity.Models;
using MediatR;

namespace Identity.Commands;

public class Revoke : IRequest
{
    public string Username { get; set; }
}
