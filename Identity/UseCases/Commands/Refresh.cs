namespace Identity.Api.Commands;

using Identity.Core.Models;
using MediatR;

public class Refresh : IRequest<TokensPair?>
{
    public string Token { get; set; }
}
