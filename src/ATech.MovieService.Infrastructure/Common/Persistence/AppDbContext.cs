using System.Reflection;

using Microsoft.EntityFrameworkCore;

namespace ATech.MovieService.Infrastructure.Common.Persistence;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    // public DbSet<Trip> Trips { get; init; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder // Carica tutte le interfacce IEntityTypeConfiguration<TEntity>
            .ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
