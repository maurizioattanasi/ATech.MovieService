using ATech.MovieService.Api.Movies.Dto;

namespace ATech.MovieService.Api.Movies.Update;

public record UpdateMovieRequest(string Id, UpdateMovieDto Movie);
