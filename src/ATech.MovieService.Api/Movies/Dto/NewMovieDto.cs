namespace ATech.MovieService.Api.Movies.Dto;

public record NewMovieDto(string Title, string? Rated, string? Plot);

public record UpdateMovieDto(string? Title, string? Rated, string? Plot);
