
using System;
using System.Threading;
using System.Threading.Tasks;

using ATech.MovieService.Application.Movies.Interfaces;
using ATech.MovieService.Domain.Movies;
using ATech.MovieService.Infrastructure.Common.Persistence;
using ATech.Repository.EntityFrameworkCore;

using MongoDB.Bson;

namespace ATech.MovieService.Infrastructure.Movies;

public class MovieRepository(AppDbContext context)
    : Repository<Movie, string>(context), IMovieRepository
{
    public async Task<Movie?> CreateMovieAsync(Movie movie, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(movie, nameof(movie));
        
        var newMovie = new Movie
        {
            Id = ObjectId.GenerateNewId().ToString(),
            Title = movie.Title,
            Rated = movie.Rated,
            Plot = movie.Plot
        };

        await context.Movies.AddAsync(newMovie, cancellationToken).ConfigureAwait(false);
        await context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        return await context.Movies.FindAsync(new object[] { newMovie.Id }, cancellationToken).ConfigureAwait(false);

    }
}
