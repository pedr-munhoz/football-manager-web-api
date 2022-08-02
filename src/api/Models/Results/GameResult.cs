using api.Infrastructure.Extensions;
using api.Models.Entities;

namespace api.Models.Results;

public class GameResult
{
    public GameResult()
    {
    }

    public GameResult(Game entity)
    {
        Id = entity.Id.ToStringId();
        Date = entity.Date;
        HomeTeam = entity.HomeTeam;
        AwayTeam = entity.AwayTeam;
        Location = entity.Location;
    }

    public string Id { get; set; } = null!;
    public DateTime? Date { get; set; }
    public string HomeTeam { get; set; } = null!;
    public string AwayTeam { get; set; } = null!;
    public string? Location { get; set; }
}
