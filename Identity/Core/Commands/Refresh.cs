using Identity.Infrastructure.Models;

namespace Identity.Core.Commands;

using MediatR;

public class Refresh : IRequest<TokensPair?>
{
    public string Token { get; set; }
}
