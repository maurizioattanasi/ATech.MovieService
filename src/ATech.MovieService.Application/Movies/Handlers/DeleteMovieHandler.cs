
using System;
using System.Threading;
using System.Threading.Tasks;

using ATech.MovieService.Application.Common.Exceptions;
using ATech.MovieService.Application.Movies.Commands;
using ATech.MovieService.Application.Movies.Interfaces;

using MediatR;

using Microsoft.Extensions.Logging;

namespace ATech.MovieService.Application.Movies.Handlers;

public class DeleteMovieHandler(IMovieRepository repository, ILogger<DeleteMovieHandler> logger) : IRequestHandler<DeleteMovieCommand>
{
    public async Task Handle(DeleteMovieCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request, nameof(request));
        
        try
        {
            var movie = await repository.GetByIdAsync(request.Id, cancellationToken).ConfigureAwait(false);

            if (movie is null)
            {
                throw new ItemNotFoundException($"No movie found with id: {request.Id}  - Unable to delete.");
            }

            repository.Remove(movie);

            await repository.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while deleting movie with id: {Id}", request.Id);
            throw;
        }
    }
}