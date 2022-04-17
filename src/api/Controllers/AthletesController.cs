using api.Models.Results;
using api.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class AthletesController : ControllerBase
{
    [HttpGet, Route("")]
    public async Task<IActionResult> Get([FromQuery] OffsetViewModel model)
    {
        await Task.CompletedTask;
        var itens = new List<AthleteResult>();
        for (int i = 0; i < (model.Length ?? 30); i++)
        {
            itens.Add(new AthleteResult());
        }

        var result = new ListResult<AthleteResult>(itens, itens.Count + new Random().Next(100));
        return Ok(result);
    }

    [HttpPost, Route("")]
    public async Task<IActionResult> Create([FromBody] AthleteViewModel model)
    {
        await Task.CompletedTask;
        var entity = model.Map();
        var result = new AthleteResult(entity);
        return Created($"/{result.Id}", result);
    }

    [HttpPut, Route("")]
    public async Task<IActionResult> Update([FromBody] AthleteViewModel model)
    {
        await Task.CompletedTask;
        var entity = model.Map();
        var result = new AthleteResult(entity);
        return Ok(result);
    }

    [HttpDelete, Route("{id}")]
    public async Task<IActionResult> Remove([FromRoute] string id)
    {
        await Task.CompletedTask;
        return NoContent();
    }
}
