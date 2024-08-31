using ATech.MovieService.Domain.Movies;
using ATech.Repository;

namespace ATech.MovieService.Application.Movies;

public interface IMovieRepository : IRepository<Movie, string>
{

}
