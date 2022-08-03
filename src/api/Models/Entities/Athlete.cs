using System.Collections;

namespace api.Models.Entities;

public class Athlete
{
    public long Id { get; set; }
    public int Number { get; set; }
    public string Name { get; set; } = null!;
    public bool Quarterback { get; set; }
    public bool RunningBack { get; set; }
    public bool TightEnd { get; set; }
    public bool Reciever { get; set; }
}
