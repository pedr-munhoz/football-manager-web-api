using System.ComponentModel.DataAnnotations;
using api.Models.Entities;

namespace api.Models.ViewModels;

public class AthleteViewModel
{
    public string Id { get; set; } = "";

    [Required]
    public int? Number { get; set; }

    [Required]
    public string Name { get; set; } = "";

    public bool Quarterback { get; set; }

    public bool RunningBack { get; set; }

    public bool TightEnd { get; set; }

    public bool Reciever { get; set; }

    public Athlete Map()
    {
        ArgumentNullException.ThrowIfNull(Number);

        return new Athlete
        {
            Number = Number.Value,
            Name = Name,
            Quarterback = Quarterback,
            RunningBack = RunningBack,
            TightEnd = TightEnd,
            Reciever = Reciever,
        };
    }
}
