using ATech.Endpoints;
using ATech.MovieService.Application.Common.Exceptions;
using ATech.MovieService.Application.Movies.Commands;

using MediatR;


namespace ATech.MovieService.Api.Movies.Delete;

public class DeleteMovieEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("api/movies/{id}", HandleAsync).WithTags("movies").AllowAnonymous();
    }

    public static async Task<IResult> HandleAsync(IMediator mediator, ILogger<DeleteMovieEndpoint> logger, string id, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            await mediator.Send(new DeleteMovieCommand(id), cancellationToken);

            return Results.Ok();
        }
        catch (ItemNotFoundException)
        {
            return Results.NotFound();
        }
        catch (Exception ex)
        {
            // Handle exception
            logger.LogError(ex, "An error occurred while deleting a movie.");

            // Return HTTP 500 Internal Server Error
            return Results.BadRequest();
        }
    }
}