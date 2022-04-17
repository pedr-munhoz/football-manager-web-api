using api.Models.Results;
using api.Models.ViewModels;
using api.Services;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class AthletesController : ControllerBase
{
    private readonly AthletesService _service;

    public AthletesController(AthletesService service)
    {
        _service = service;
    }

    [HttpGet, Route("")]
    public async Task<IActionResult> Get([FromQuery] OffsetViewModel model)
    {
        var result = await _service.List(model);
        return Ok(result);
    }

    [HttpPost, Route("")]
    public async Task<IActionResult> Create([FromBody] AthleteViewModel model)
    {
        var result = await _service.Create(model);
        return Created($"/{result.Content?.Id}", result);
    }

    [HttpPut, Route("")]
    public async Task<IActionResult> Update([FromBody] AthleteViewModel model)
    {
        var result = await _service.Update(model);
        return Ok(result);
    }

    [HttpDelete, Route("{id}")]
    public async Task<IActionResult> Remove([FromRoute] string id)
    {
        var result = await _service.Remove(id);

        if (result.Success)
            return NoContent();

        return NotFound(result.Error);
    }
}
