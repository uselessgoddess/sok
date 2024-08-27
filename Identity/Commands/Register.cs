using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Commands;

public class Register : IRequest<EmptyResult?>
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
}

public class RegisterValidation : AbstractValidator<Register>
{
    public RegisterValidation()
    {
        RuleFor(x => x.Username)
            .NotEmpty()
            .Length(3, 50);

        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(6);

        RuleFor(x => x.Email)
            .EmailAddress();
    }
}