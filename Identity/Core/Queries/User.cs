namespace Identity.Core.Queries;

using Identity.Infrastructure.Models;
using MediatR;

public class User : IRequest<AppUser>
{
    public string Username { get; set; }
}