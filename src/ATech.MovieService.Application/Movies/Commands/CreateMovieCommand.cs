using ATech.MovieService.Domain.Movies;

using MediatR;

namespace ATech.MovieService.Application.Movies.Commands;

public record CreateMovieCommand(string Title, string? Rated, string? Plot) : IRequest<Movie?>;