using api.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace api.Infrastructure.Database;

public class ServerDbContext : DbContext
{
    public ServerDbContext(DbContextOptions options) : base(options) { }

    public DbSet<Athlete> Athletes { get; set; } = null!;
}
