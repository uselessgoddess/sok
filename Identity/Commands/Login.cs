using FluentValidation;
using Identity.Models;
using MediatR;

namespace Identity.Commands;

public class Login : IRequest<TokensPair?>
{
    public string Username { get; set; }
    public string Password { get; set; }
}

public class LoginValidation : AbstractValidator<Login>
{
    public LoginValidation()
    {
        RuleFor(x => x.Username)
            .NotEmpty()
            .Length(3, 50);

        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(6);
    }
}
