
using ATech.MovieService.Application.Movies.Interfaces;
using ATech.MovieService.Domain.Movies;

using MediatR;

using Microsoft.Extensions.Logging;

namespace ATech.MovieService.Application.Movies.Queries;

public record GetMovieByIdQuery(string Id) : IRequest<Movie?>;

public class GetMovieByIdHandler(IMovieRepository movieRepository, ILogger<GetMovieByIdHandler> logger) : IRequestHandler<GetMovieByIdQuery, Movie?>
{
    public async Task<Movie?> Handle(GetMovieByIdQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("returning movie with id: {Id}", request.Id);

        var movie = await movieRepository.GetByIdAsync(request.Id, cancellationToken);

        return movie; // Replace with actual implementation using movieRepository
    }
}
