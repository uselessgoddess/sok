namespace VRisc.Presentation.Validations;

using FluentValidation;
using VRisc.Api.DTOs;

public class EmulationStateDtoValidation : AbstractValidator<EmulationStateDto>
{
    public EmulationStateDtoValidation()
    {
        RuleFor(x => x.Cpu.Bus.Dram).Must(IsBase64String).WithMessage("must be base64 string");
    }

    private static bool IsBase64String(string base64)
    {
        var buffer = new Span<byte>(new byte[base64.Length]);
        return Convert.TryFromBase64String(base64, buffer, out _);
    }
}