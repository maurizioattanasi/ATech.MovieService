using System;
using System.Reflection;

using ATech.MovieService.Domain.Movies;

using Microsoft.EntityFrameworkCore;

namespace ATech.MovieService.Infrastructure.Common.Persistence;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Movie> Movies { get; init; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ArgumentNullException.ThrowIfNull(modelBuilder, nameof(modelBuilder));

        base.OnModelCreating(modelBuilder);

        modelBuilder // Carica tutte le interfacce IEntityTypeConfiguration<TEntity>
            .ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
