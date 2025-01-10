
using ATech.MovieService.Application.Common.Exceptions;
using ATech.MovieService.Application.Movies.Commands;
using ATech.MovieService.Application.Movies.Interfaces;

using MediatR;

using Microsoft.Extensions.Logging;

public class DeleteMovieHandler(IMovieRepository repository, ILogger<DeleteMovieHandler> logger) : IRequestHandler<DeleteMovieCommand>
{
    public async Task Handle(DeleteMovieCommand request, CancellationToken cancellationToken)
    {
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