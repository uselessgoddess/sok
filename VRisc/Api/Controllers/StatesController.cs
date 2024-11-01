using MediatR;
using VRisc.Core.Entities;
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

    [HttpPost("new")]
    public async Task<IActionResult> New()
    {
        return Ok(await mediator.Send(new NewState { User = AuthUser }));
    }

    [HttpGet("load")]
    public async Task<EmulationStateDto?> Load(string id)
    {
        var state = await mediator.Send(new LoadState
        {
            User = AuthUser,
            Id = id,
        });
        return mapper.Map<EmulationStateDto>(state);
    }

    [HttpPut("store")]
    public async Task Store([FromBody] EmulationInputDto dto)
    {
        await mediator.Send(new StoreState
        {
            State = new EmulationState(AuthUser, dto.Name)
            {
                Id = dto.Id,
                Creation = null,
                Modified = null,
                Cpu = mapper.Map<CpuState>(dto.Cpu),
            },
        });
    }

    [HttpDelete("remove")]
    public async Task Remove([FromBody] string id)
    {
        await mediator.Send(new RemoveState
        {
            User = AuthUser,
            Id = id,
        });
    }

    [HttpPost("compile-code")]
    public async Task<IActionResult> CompileCode([FromBody] string code)
    {
        return Ok(await mediator.Send(new CompileCode
        {
            Code = code,
            Jwt = (await HttpContext.GetTokenAsync("Bearer", "access_token"))!,
        }));
    }

    [HttpGet("sessions")]
    public async Task<IActionResult> Sessions(uint page, uint size)
    {
        var list = await mediator.Send(new StateSessions
        {
            User = AuthUser,
            Page = page,
            Size = size,
        });
        return Ok(list.Select(mapper.Map<EmulationInfoDto>).ToList());
    }
}