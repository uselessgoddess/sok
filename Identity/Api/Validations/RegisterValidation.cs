namespace Identity.Api.Commands;

using FluentValidation;

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