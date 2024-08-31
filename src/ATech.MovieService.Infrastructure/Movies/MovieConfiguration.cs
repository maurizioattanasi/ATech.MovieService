using ATech.MovieService.Domain.Movies;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using MongoDB.Bson;

namespace ATech.MovieService.Infrastructure.Movies;

public class MovieConfiguration : IEntityTypeConfiguration<Movie>
{
    public void Configure(EntityTypeBuilder<Movie> builder)
    {
        builder
            .HasKey(e => e.Id);

        builder
            .Property(e=>e.Id)
            .HasConversion(
                v => new ObjectId(v),
                v => v.ToString()
            );
    }
}