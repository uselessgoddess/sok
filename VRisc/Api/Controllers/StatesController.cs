using MediatR;
using VRisc.UseCases.Commands;
using VRisc.UseCases.Queries;

namespace VRisc.Api.Controllers;

using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VRisc.Api.DTOs;

[ApiController]
[Authorize]
[Route("api/states")]
public class StatesController(IMediator mediator, IMapper mapper) : ControllerBase
{
    private string AuthUser => HttpContext.User.Identity!.Name!;

    [HttpGet("/current")]
    public async Task<IActionResult> Current()
    {
        return Ok(await mediator.Send(new CurrentState { User = AuthUser }));
    }

    [HttpPost("/new")]
    public async Task<IActionResult> New()
    {
        return Ok(await mediator.Send(new NewState { User = AuthUser }));
    }

    [HttpPost("/save")]
    public async Task<IActionResult> Save()
    {
        return Ok(await mediator.Send(new SaveState { User = AuthUser }));
    }

    [HttpPut("/update-current")]
    public async Task UpdateCurrent([FromBody] EmulationStateDto dto)
    {
        await mediator.Send(new UpdateState
        {
            User = AuthUser,
            Update = state => mapper.Map(dto, state),
        });
    }

    [HttpPut("/load")]
    public async Task Load(string id)
    {
        await mediator.Send(new LoadState
        {
            User = AuthUser,
            Id = id,
        });
    }

    [HttpPut("/load-dram")]
    public async Task LoadDram([FromBody] byte[] dram)
    {
        await mediator.Send(new LoadDram
        {
            User = AuthUser,
            Dram = dram,
        });
    }

    [HttpPut("/load-code")]
    public async Task LoadCode([FromBody] string code)
    {
        await mediator.Send(new LoadCode
        {
            User = AuthUser,
            Code = code,
            Jwt = (await HttpContext.GetTokenAsync("Bearer", "access_token"))!,
        });
    }

    [HttpGet("/sessions")]
    public async Task<IActionResult> Sessions(uint page, uint size)
    {
        return Ok(mediator.Send(new StateSessions
        {
            User = AuthUser,
            Page = page,
            Size = size,
        }));
    }
}