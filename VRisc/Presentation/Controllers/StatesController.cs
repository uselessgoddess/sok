namespace VRisc.Presentation.Controllers;

using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VRisc.Core.Entities;
using VRisc.Core.Exceptions;
using VRisc.Core.Interfaces;
using VRisc.Presentation.DTOs;

[ApiController]
[Authorize]
[Route("api/states")]
public class StatesController(
    IEmulationStatesService states,
    IEmulationStateRepository repo,
    IMapper mapper)
    : ControllerBase
{
    private string AuthUser => HttpContext.User.Identity!.Name!;

    [HttpGet("/current")]
    public async Task<IActionResult> Current()
    {
        return Ok(states.GetState(AuthUser));
    }

    [HttpPost("/new")]
    public async Task<IActionResult> New()
    {
        var state = new EmulationState(AuthUser);
        states.SetState(AuthUser, state);

        return Ok(state);
    }

    [HttpPut("/update-current")]
    public async Task UpdateCurrent([FromBody] EmulationStateDto dto)
    {
        states.UpdateState(AuthUser, state => mapper.Map(dto, state));
    }

    [HttpPut("/load")]
    public async Task Load(string id)
    {
        var state = await repo.LoadState(id);

        states.SetState(AuthUser, state);
    }

    [HttpPut("/load-dram")]
    public async Task LoadDram([FromBody] byte[] dram)
    {
        states.UpdateState(AuthUser, state =>
        {
            state.Cpu.Bus.Dram = dram;
            return state;
        });
    }

    [HttpGet("/sessions")]
    public async Task<IActionResult> Sessions(uint page, uint size)
    {
        var list = await repo.LoadStates(AuthUser);

        return Ok(list.Skip((int)(page * size)).Take((int)size));
    }
}