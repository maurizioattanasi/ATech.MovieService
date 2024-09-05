using MediatR;

namespace ATech.MovieService.Application.Movies.Commands;

public record DeleteMovieCommand(string Id) : IRequest;