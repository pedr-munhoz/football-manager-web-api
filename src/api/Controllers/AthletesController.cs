using api.Models.Results;
using api.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class AthletesController : ControllerBase
{
    [HttpGet, Route("")]
    public async Task<IActionResult> Get()
    {
        await Task.CompletedTask;
        var result = new List<AthleteResult> { new AthleteResult() };
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
}
