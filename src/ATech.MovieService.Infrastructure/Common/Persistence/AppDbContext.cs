using System.Reflection;

using ATech.MovieService.Domain.Movies;

using Microsoft.EntityFrameworkCore;

using MongoDB.EntityFrameworkCore.Extensions;

namespace ATech.MovieService.Infrastructure.Common.Persistence;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Movie> Movies { get; init; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder // Carica tutte le interfacce IEntityTypeConfiguration<TEntity>
            .ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
