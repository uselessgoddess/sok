using Identity.Models;
using MediatR;

namespace Identity.Commands;

public class Users : IRequest<List<AppUser>>
{
    public uint Page { get; set; }
    public uint Count { get; set; }
}