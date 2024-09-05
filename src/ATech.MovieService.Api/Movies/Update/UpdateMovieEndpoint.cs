using ATech.MovieService.Api.Movies.Dto;
using ATech.MovieService.Application.Common.Exceptions;
using ATech.MovieService.Application.Movies.Commands;

using FastEndpoints;

using MediatR;

namespace ATech.MovieService.Api.Movies.Update;

public class UpdateMovieEndpoint(IMediator mediator, ILogger<UpdateMovieEndpoint> logger) : Endpoint<UpdateMovieRequest, UpdateMovieRequestResponse>
{
    public override void Configure()
    {
        Patch("api/movies/{id}");

        Options(x => x.WithTags("Movies"));

        AllowAnonymous(Http.PATCH);
    }

    public override async Task HandleAsync(UpdateMovieRequest req, CancellationToken ct)
    {
        try
        {
            var command = new UpdateMovieCommand(req.Id, req.Movie.Title, req.Movie.Rated, req.Movie.Plot);

            var updatedMovie = await mediator.Send(command, ct);

            if (updatedMovie is null)
            {
                await SendNotFoundAsync(ct);
                return;
            }

            Response = new UpdateMovieRequestResponse(MovieMapper.ToDto(updatedMovie));
        }
        catch (ItemNotFoundException)
        {
            await SendNotFoundAsync(ct);
        }
        catch (Exception ex)
        {
            // Handle exception
            logger.LogError(ex, "An error occurred while updating a movie.");

            // Return HTTP 500 Internal Server Error
            await SendErrorsAsync(StatusCodes.Status500InternalServerError, ct);
        }
    }
}
