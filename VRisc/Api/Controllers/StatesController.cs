namespace VRisc.Api.Controllers;

using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VRisc.Api.DTOs;
using VRisc.UseCases.Handlers;

[ApiController]
[Authorize]
[Route("api/states")]
public class StatesController(StatesHandler handler, IMapper mapper) : ControllerBase
{
    private string AuthUser => HttpContext.User.Identity!.Name!;

    [HttpGet("/current")]
    public async Task<IActionResult> Current()
    {
        return Ok(handler.Current(AuthUser));
    }

    [HttpPost("/new")]
    public async Task<IActionResult> New()
    {
        return Ok(handler.New(AuthUser));
    }

    [HttpPost("/save")]
    public async Task<IActionResult> Save()
    {
        return Ok(handler.Save(AuthUser));
    }

    [HttpPut("/update-current")]
    public async Task UpdateCurrent([FromBody] EmulationStateDto dto)
    {
        handler.UpdateCurrent(AuthUser, state => mapper.Map(dto, state));
    }

    [HttpPut("/load")]
    public async Task Load(string id)
    {
        await handler.LoadAsync(AuthUser, id);
    }

    [HttpPut("/load-dram")]
    public async Task LoadDram([FromBody] byte[] dram)
    {
        handler.LoadDram(AuthUser, dram);
    }

    [HttpGet("/sessions")]
    public async Task<IActionResult> Sessions(uint page, uint size)
    {
        return Ok(handler.Sessions(AuthUser, page, size));
    }
}