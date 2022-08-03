using api.Models.Results;
using api.Models.ViewModels;
using api.Services;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class GamesController : ControllerBase
{
    private readonly GamesService _service;

    public GamesController(GamesService service)
    {
        _service = service;
    }

    [HttpGet, Route("")]
    public async Task<IActionResult> Get([FromQuery] OffsetViewModel model)
    {
        var serviceResult = await _service.List(model);

        var result = new ListResult<GameResult>(
            serviceResult.Content?.Select(x => new GameResult(x)),
            serviceResult.Count
        );

        return Ok(result);
    }

    [HttpGet, Route("{id}")]
    public async Task<IActionResult> Get([FromRoute] string id)
    {
        var result = await _service.Find(id);

        if (result.Success && result.Content != null)
            return Created($"/{result.Content.Id}", new GameDetailResult(result.Content));

        return NotFound(result.Error);
    }

    [HttpPost, Route("")]
    public async Task<IActionResult> Create([FromBody] GameViewModel model)
    {
        var result = await _service.Create(model);

        if (result.Success && result.Content != null)
            return Created($"/{result.Content.Id}", new GameResult(result.Content));

        return UnprocessableEntity(result.Error);
    }

    [HttpPut, Route("")]
    public async Task<IActionResult> Update([FromBody] GameViewModel model)
    {
        var result = await _service.Update(model);

        if (result.Success && result.Content != null)
            return Ok(new GameResult(result.Content));

        if (result.Error != null && result.Error.Code == 404)
            return NotFound(result.Error);

        return UnprocessableEntity(result.Error);
    }

    [HttpPatch, Route("{gameId}/home-team/athletes/{athleteId}")]
    public async Task<IActionResult> AddAthleteToHomeTeam([FromRoute] string gameId, string athleteId)
    {
        var result = await _service.AddAthlete(gameId, athleteId, isHomeTeam: true);

        if (result.Success && result.Content != null)
            return Ok(new GameDetailResult(result.Content));

        return UnprocessableEntity(result.Error);
    }

    [HttpPatch, Route("{gameId}/away-team/athletes/{athleteId}")]
    public async Task<IActionResult> AddAthleteToAwayTeam([FromRoute] string gameId, string athleteId)
    {
        var result = await _service.AddAthlete(gameId, athleteId, isHomeTeam: false);

        if (result.Success && result.Content != null)
            return Ok(new GameDetailResult(result.Content));

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
