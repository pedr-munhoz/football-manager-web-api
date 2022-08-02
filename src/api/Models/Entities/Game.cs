namespace api.Models.Entities
{
    public class Game
    {
        public long Id { get; set; }
        public DateTime? Date { get; set; }
        public string HomeTeam { get; set; } = null!;
        public string AwayTeam { get; set; } = null!;
        public string? Location { get; set; }
    }
}