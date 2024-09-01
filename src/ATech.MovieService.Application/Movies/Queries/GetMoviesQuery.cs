
using ATech.MovieService.Application.Movies.Interfaces;
using ATech.MovieService.Domain.Movies;

using MediatR;

using Microsoft.Extensions.Logging;

namespace ATech.MovieService.Application.Movies.Queries;

public record GetMoviesQuery() : IRequest<IEnumerable<Movie>>;

public class GetMoviesHandler(IMovieRepository repository, ILogger<GetMoviesHandler> logger) : IRequestHandler<GetMoviesQuery, IEnumerable<Movie>>
{
    public async Task<IEnumerable<Movie>> Handle(GetMoviesQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Returning movies...");

        var movies = await repository.GetAllAsync(cancellationToken);

        return movies;
    }

}

