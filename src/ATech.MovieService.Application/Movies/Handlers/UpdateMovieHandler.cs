
using System;
using System.Threading;
using System.Threading.Tasks;

using ATech.MovieService.Application.Common.Exceptions;
using ATech.MovieService.Application.Movies.Commands;
using ATech.MovieService.Application.Movies.Interfaces;
using ATech.MovieService.Domain.Movies;

using MediatR;

using Microsoft.Extensions.Logging;

namespace ATech.MovieService.Application.Movies.Handlers;

public class UpdateMovieHandler(IMovieRepository repository, ILogger<UpdateMovieHandler> logger) : IRequestHandler<UpdateMovieCommand, Movie?>
{
    public async Task<Movie?> Handle(UpdateMovieCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request, nameof(request));

        try
        {
            var movie = await repository.GetByIdAsync(request.Id, cancellationToken).ConfigureAwait(false);

            if (movie is null)
            {
                throw new ItemNotFoundException($"No movie found with id: {request.Id}  - Unable to update.");
            }

            if (request.Title is not null)
            {
                movie.Title = request.Title;
            }

            if (request.Rated is not null)
            {
                movie.Rated = request.Rated;
            }

            if (request.Plot is not null)
            {
                movie.Plot = request.Plot;
            }

            await repository.UpdateAsync(movie, cancellationToken).ConfigureAwait(false);
            
            await repository.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return movie;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while updating movie with id: {Id}", request.Id);
            throw;
        }
    }
}