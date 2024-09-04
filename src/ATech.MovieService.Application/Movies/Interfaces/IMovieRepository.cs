using ATech.MovieService.Domain.Movies;
using ATech.Repository;

namespace ATech.MovieService.Application.Movies.Interfaces;

public interface IMovieRepository : IRepository<Movie, string>
{
    Task<Movie?> CreateMovieAsync(Movie movie, CancellationToken cancellationToken = default(CancellationToken)); 
}
