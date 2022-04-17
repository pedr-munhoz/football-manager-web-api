using api.Infrastructure.Database;
using api.Infrastructure.Extensions;
using api.Models.Entities;
using api.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace api.Services;

public class AthletesService
{
    private readonly ServerDbContext _dbContext;

    public AthletesService(ServerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ServiceListResult<Athlete>> List(OffsetViewModel offset)
    {
        var count = await _dbContext.Athletes.LongCountAsync();
        var itens = await _dbContext.Athletes
            .OrderBy(x => x.Id)
            .Skip(offset.Index)
            .Take(offset.Length ?? 10)
            .ToListAsync();

        return new ServiceListResult<Athlete>(itens, count);
    }

    public async Task<ServiceResult<Athlete>> Create(AthleteViewModel model)
    {
        var entity = model.Map();
        await _dbContext.Athletes.AddAsync(entity);

        await _dbContext.SaveChangesAsync();

        return new ServiceResult<Athlete>(entity);
    }

    public async Task<ServiceResult<Athlete>> Update(AthleteViewModel model)
    {
        var id = model.Id.ToLongId();
        var entity = await _dbContext.Athletes.Where(x => x.Id == id).FirstOrDefaultAsync();

        if (entity == null)
        {
            var error = new ServiceError($"{typeof(Athlete).Name} not found", $"No {typeof(Athlete).Name} could be located for id: {id}", 404);
            return new ServiceResult<Athlete>(error);
        }

        var newEntity = model.Map();

        entity.Number = newEntity.Number;
        entity.Name = newEntity.Name;
        entity.Quarterback = newEntity.Quarterback;
        entity.RunningBack = newEntity.RunningBack;
        entity.TightEnd = newEntity.TightEnd;
        entity.Reciever = newEntity.Reciever;

        await _dbContext.SaveChangesAsync();

        return new ServiceResult<Athlete>(entity);
    }

    public async Task<ServiceResult> Remove(string stringId)
    {
        var id = stringId.ToLongId();
        var entity = await _dbContext.Athletes.Where(x => x.Id == id).FirstOrDefaultAsync();

        if (entity == null)
        {
            var error = new ServiceError($"{typeof(Athlete).Name} not found", $"No {typeof(Athlete).Name} could be located for id: {id}", 404);
            return new ServiceResult(false, error);
        }

        _dbContext.Remove(entity);

        await _dbContext.SaveChangesAsync();

        return new ServiceResult<Athlete>(entity);
    }
}
