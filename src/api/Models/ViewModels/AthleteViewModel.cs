using api.Models.Entities;

namespace api.Models.ViewModels;

public class AthleteViewModel
{
    public string Id { get; set; } = "";
    public int Number { get; set; }
    public string Name { get; set; } = "";
    public bool Quarterback { get; set; }
    public bool RunningBack { get; set; }
    public bool TightEnd { get; set; }
    public bool Reciever { get; set; }

    public Athlete Map()
    {
        return new Athlete
        {
            Number = Number,
            Name = Name,
            Quarterback = Quarterback,
            RunningBack = RunningBack,
            TightEnd = TightEnd,
            Reciever = Reciever,
        };
    }
}
