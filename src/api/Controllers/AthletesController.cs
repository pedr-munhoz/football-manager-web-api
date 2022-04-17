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
        var serviceResult = await _service.List(model);

        var result = new ListResult<AthleteResult>(
            serviceResult.Content?.Select(x => new AthleteResult(x)),
            serviceResult.Count
        );

        return Ok(result);
    }

    [HttpPost, Route("")]
    public async Task<IActionResult> Create([FromBody] AthleteViewModel model)
    {
        var result = await _service.Create(model);

        if (result.Success && result.Content != null)
            return Created($"/{result.Content.Id}", new AthleteResult(result.Content));

        return UnprocessableEntity(result.Error);
    }

    [HttpPut, Route("")]
    public async Task<IActionResult> Update([FromBody] AthleteViewModel model)
    {
        var result = await _service.Update(model);

        if (result.Success && result.Content != null)
            return Ok(new AthleteResult(result.Content));
        else if (result.Error != null && result.Error.Code == 404)
            return NotFound(result.Error);

        return UnprocessableEntity(result.Error);
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
