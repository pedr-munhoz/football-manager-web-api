using api.Models.Entities;

namespace api.Models.Results
{
    public class GameDetailResult : GameResult
    {
        public GameDetailResult()
        {
        }

        public GameDetailResult(Game entity) : base(entity)
        {
            HomeTeamAthletes = entity.HomeTeamAthletes.Select(x => new AthleteResult(x)).ToList();
            AwayTeamAthletes = entity.AwayTeamAthletes.Select(x => new AthleteResult(x)).ToList();
        }

        public ICollection<AthleteResult> HomeTeamAthletes { get; set; } = null!;
        public ICollection<AthleteResult> AwayTeamAthletes { get; set; } = null!;
    }
}