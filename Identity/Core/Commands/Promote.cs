using FluentValidation;
using Identity.Core.Models;
using MediatR;

namespace Identity.Core.Commands;

public class Promote : IRequest
{
    public string Username { get; set; }
    public IEnumerable<string> Roles { get; set; }
}

public class PromoteValidation : AbstractValidator<Promote>
{
    public PromoteValidation()
    {
        RuleForEach(x => x.Roles)
            .Must(role => Enum.TryParse<Role>(role, true, out _)).WithMessage("invalid role");
    }
}