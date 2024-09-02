namespace Identity.Core.Queries;

using Identity.Core.Models;
using MediatR;

public class Users : IRequest<List<AppUser>>
{
    public uint Page { get; set; }
    public uint Size { get; set; }
}