using ATech.MovieService.Domain.Movies;

using MediatR;

namespace ATech.MovieService.Application.Movies.Commands;

public record UpdateMovieCommand(string Id, string? Title, string? Rated, string? Plot) : IRequest<Movie?>;
