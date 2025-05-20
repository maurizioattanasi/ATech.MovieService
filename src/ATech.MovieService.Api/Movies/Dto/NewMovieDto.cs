namespace ATech.MovieService.Api.Movies.Dto;

internal sealed record NewMovieDto(string Title, string? Rated, string? Plot);

internal sealed record UpdateMovieDto(string? Title, string? Rated, string? Plot);
