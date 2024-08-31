using ATech.MovieService.Application.Movies;
using ATech.MovieService.Domain.Movies;
using ATech.MovieService.Infrastructure.Common.Persistence;
using ATech.Repository.EntityFrameworkCore;

namespace ATech.MovieService.Infrastructure.Movies;

public class MovieRepository(AppDbContext context) 
    : Repository<Movie, string>(context), IMovieRepository
    { }
