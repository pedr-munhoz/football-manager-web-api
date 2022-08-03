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
            Athletes = entity.Athletes.Select(x => new AthleteResult(x)).ToList();
        }

        public ICollection<AthleteResult> Athletes { get; set; } = null!;
    }
}