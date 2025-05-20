using ATech.MovieService.Domain.Movies;

namespace ATech.MovieService.Api.Movies.Dto;

internal static class MovieMapper
{
    public static MovieDto ToDto(Movie movie) => new MovieDto(movie.Id, movie.Title, movie.Rated, movie.Plot);
}
