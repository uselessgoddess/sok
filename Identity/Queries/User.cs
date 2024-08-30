using FluentValidation;
using Identity.Models;
using MediatR;

namespace Identity.Commands;

public class User : IRequest<AppUser>
{
    public string Username { get; set; }
}