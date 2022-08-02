using System.ComponentModel.DataAnnotations;
using api.Infrastructure.Extensions;
using api.Models.Entities;

namespace api.Models.ViewModels;

public class GameViewModel
{
    public string? Id { get; set; }

    public DateTime Date { get; set; }

    [Required]
    public string HomeTeam { get; set; } = null!;

    [Required]
    public string AwayTeam { get; set; } = null!;

    public string? Location { get; set; }

    public Game Map()
    {
        return new Game
        {
            Date = Date.SetKindUtc(),
            HomeTeam = HomeTeam,
            AwayTeam = AwayTeam,
            Location = Location,
        };
    }
}
