using System;
using System.Threading;
using System.Threading.Tasks;

using ATech.MovieService.Application.Movies.Commands;
using ATech.MovieService.Application.Movies.Interfaces;
using ATech.MovieService.Domain.Movies;

using MediatR;

using Microsoft.Extensions.Logging;

namespace ATech.MovieService.Application.Movies.Handlers;

public class CreateMovieHandler(IMovieRepository repository, ILogger<CreateMovieHandler> logger) : IRequestHandler<CreateMovieCommand, Movie?>
{
    public async Task<Movie?> Handle(CreateMovieCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request, nameof(request));

        try
        {
            logger.LogInformation("Creating new movie: {Title}", request.Title);

            var movie = new Movie
            {
                Title = request.Title,
                Rated = request.Rated,
                Plot = request.Plot
            };

            return await repository.CreateMovieAsync(movie, cancellationToken).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while creating a new movie: {Title}", request.Title);
            throw;
        }
    }
}
