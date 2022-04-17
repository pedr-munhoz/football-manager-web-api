using api.Infrastructure.Extensions;
using api.Models.Entities;

namespace api.Models.Results;

public class AthleteResult
{
    public AthleteResult()
    {
    }

    public AthleteResult(Athlete entity)
    {
        Id = entity.Id.ToStringId();
        Number = entity.Number;
        Name = entity.Name;
        Quarterback = entity.Quarterback;
        RunningBack = entity.RunningBack;
        TightEnd = entity.TightEnd;
        Reciever = entity.Reciever;
    }

    public string Id { get; set; } = "";
    public int Number { get; set; }
    public string Name { get; set; } = "";
    public bool Quarterback { get; set; }
    public bool RunningBack { get; set; }
    public bool TightEnd { get; set; }
    public bool Reciever { get; set; }
}
