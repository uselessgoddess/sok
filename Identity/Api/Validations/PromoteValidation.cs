namespace Identity.Api.Validations;

using FluentValidation;
using Identity.Api.Commands;
using Identity.Core.Models;

public class PromoteValidation : AbstractValidator<Promote>
{
    public PromoteValidation()
    {
        RuleForEach(x => x.Roles)
            .Must(role => Enum.TryParse<Role>(role, true, out _)).WithMessage("invalid role");
    }
}