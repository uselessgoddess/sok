using Microsoft.AspNetCore.Mvc;
using VRisc.Core.Entities;
using VRisc.Core.Interfaces;

namespace Presentation.Controllers;

[ApiController]
[Route("api/states")]
public class EmulationController(EmulationState state) : ControllerBase
{
    [HttpPost("start/{id}")]
    public async Task<IActionResult> StartEmulation(string id)
    {
        return Ok(state);
    }
}