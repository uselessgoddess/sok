using Identity.Core.Models;
using MediatR;

namespace Identity.Core.Commands;

public class Refresh : IRequest<TokensPair?>
{
    public string Token { get; set; }
}
