using api.Infrastructure.Database;
using api.Infrastructure.Extensions;
using api.Models.Entities;
using api.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace api.Services;

public class GamesService
{
    private readonly ServerDbContext _dbContext;

    public GamesService(ServerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ServiceListResult<Game>> List(OffsetViewModel offset)
    {
        var count = await _dbContext.Games.LongCountAsync();
        var itens = await _dbContext.Games.Skip(offset.Index).Take(offset.Length ?? 10).ToListAsync();

        return new ServiceListResult<Game>(itens, count);
    }

    public async Task<ServiceResult<Game>> Find(string stringId)
    {
        var id = stringId.ToLongId();

        var entity = await _dbContext.Games
            .Include(x => x.Athletes)
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync();

        if (entity == null)
        {
            var error = new ServiceError($"{typeof(Game).Name} not found", $"No {typeof(Game).Name} could be located for id: {id}", 404);
            return new ServiceResult<Game>(error);
        }

        return new ServiceResult<Game>(entity);
    }

    public async Task<ServiceResult<Game>> Create(GameViewModel model)
    {
        var entity = model.Map();
        await _dbContext.Games.AddAsync(entity);

        await _dbContext.SaveChangesAsync();

        return new ServiceResult<Game>(entity);
    }

    public async Task<ServiceResult<Game>> Update(GameViewModel model)
    {
        ArgumentNullException.ThrowIfNull(model.Id);
        var id = model.Id.ToLongId();
        var entity = await _dbContext.Games.Where(x => x.Id == id).FirstOrDefaultAsync();

        if (entity == null)
        {
            var error = new ServiceError($"{typeof(Game).Name} not found", $"No {typeof(Game).Name} could be located for id: {id}", 404);
            return new ServiceResult<Game>(error);
        }

        var newEntity = model.Map();

        entity.Date = newEntity.Date;
        entity.HomeTeam = newEntity.HomeTeam;
        entity.AwayTeam = newEntity.AwayTeam;
        entity.Location = newEntity.Location;

        await _dbContext.SaveChangesAsync();

        return new ServiceResult<Game>(entity);
    }

    public async Task<ServiceResult<Game>> AddAthlete(string gameId, string athleteId)
    {
        ArgumentNullException.ThrowIfNull(gameId);
        ArgumentNullException.ThrowIfNull(athleteId);

        var gameIdLong = gameId.ToLongId();
        var athleteIdLong = athleteId.ToLongId();

        var game = await _dbContext.Games
            .Include(x => x.Athletes)
            .Where(x => x.Id == gameIdLong)
            .FirstOrDefaultAsync();

        if (game == null)
        {
            var error = new ServiceError($"{typeof(Game).Name} not found", $"No {typeof(Game).Name} could be located for id: {gameIdLong}", 404);
            return new ServiceResult<Game>(error);
        }

        var athlete = await _dbContext.Athletes
            .Where(x => x.Id == athleteIdLong)
            .FirstOrDefaultAsync();

        if (athlete == null)
        {
            var error = new ServiceError($"{typeof(Athlete).Name} not found", $"No {typeof(Athlete).Name} could be located for id: {athleteIdLong}", 404);
            return new ServiceResult<Game>(error);
        }

        if (game.Athletes.Any(x => x.Id == athleteIdLong))
        {
            var error = new ServiceError($"{typeof(Athlete).Name} already exists", $"{typeof(Athlete).Name} with id: {athleteIdLong} already exists in {typeof(Game).Name} with id: {gameIdLong}", 400);
            return new ServiceResult<Game>(error);
        }

        game.Athletes.Add(athlete);
        await _dbContext.SaveChangesAsync();

        return new ServiceResult<Game>(game);
    }

    public async Task<ServiceResult<Game>> Remove(string stringId)
    {
        var id = stringId.ToLongId();
        var entity = await _dbContext.Games.Where(x => x.Id == id).FirstOrDefaultAsync();

        if (entity == null)
        {
            var error = new ServiceError($"{typeof(Game).Name} not found", $"No {typeof(Game).Name} could be located for id: {id}", 404);
            return new ServiceResult<Game>(error);
        }

        _dbContext.Games.Remove(entity);

        await _dbContext.SaveChangesAsync();

        return new ServiceResult<Game>(entity);
    }
}
