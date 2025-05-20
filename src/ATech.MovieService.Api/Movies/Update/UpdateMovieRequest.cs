using ATech.MovieService.Api.Movies.Dto;

namespace ATech.MovieService.Api.Movies.Update;

internal sealed record UpdateMovieRequest(string Id, UpdateMovieDto Movie);
