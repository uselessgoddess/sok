using Identity.Models;
using MediatR;

namespace Identity.Commands;

public class Refresh : IRequest<TokensPair?>
{
    public string Token { get; set; }
}
