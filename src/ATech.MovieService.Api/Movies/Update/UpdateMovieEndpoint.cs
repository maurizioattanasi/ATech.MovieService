using ATech.Endpoints;
using ATech.MovieService.Api.Movies.Dto;
using ATech.MovieService.Application.Common.Exceptions;
using ATech.MovieService.Application.Movies.Commands;

// using FastEndpoints;

using MediatR;


namespace ATech.MovieService.Api.Movies.Update;

public class UpdateMovieEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPatch("api/movies/{id}", HandleAsync).WithTags("movies").AllowAnonymous();
    }

    public static async Task<IResult> HandleAsync(IMediator mediator, ILogger<UpdateMovieEndpoint> logger, string id, UpdateMovieRequest request, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            var command = new UpdateMovieCommand(id, request.Movie.Title, request.Movie.Rated, request.Movie.Plot);

            var updatedMovie = await mediator.Send(command, cancellationToken);

            if (updatedMovie is null)
            {
                return Results.NoContent();
            }

            return Results.Ok(new UpdateMovieRequestResponse(MovieMapper.ToDto(updatedMovie)));
        }
        catch (ItemNotFoundException)
        {
            return Results.NotFound();
        }
        catch (Exception ex)
        {
            // Handle exception
            logger.LogError(ex, "An error occurred while updating a movie.");

            // Return HTTP 500 Internal Server Error
            return Results.BadRequest();
        }
    }
}

// public class UpdateMovieEndpoint(IMediator mediator, ILogger<UpdateMovieEndpoint> logger) : Endpoint<UpdateMovieRequest, UpdateMovieRequestResponse>
// {
//     public override void Configure()
//     {
//         Patch("api/movies/{id}");

//         Options(x => x.WithTags("Movies"));

//         AllowAnonymous(Http.PATCH);
//     }

//     public override async Task HandleAsync(UpdateMovieRequest req, CancellationToken ct)
//     {
//         try
//         {
//             var command = new UpdateMovieCommand(req.Id, req.Movie.Title, req.Movie.Rated, req.Movie.Plot);

//             var updatedMovie = await mediator.Send(command, ct);

//             if (updatedMovie is null)
//             {
//                 await SendNotFoundAsync(ct);
//                 return;
//             }

//             Response = new UpdateMovieRequestResponse(MovieMapper.ToDto(updatedMovie));
//         }
//         catch (ItemNotFoundException)
//         {
//             await SendNotFoundAsync(ct);
//         }
//         catch (Exception ex)
//         {
//             // Handle exception
//             logger.LogError(ex, "An error occurred while updating a movie.");

//             // Return HTTP 500 Internal Server Error
//             await SendErrorsAsync(StatusCodes.Status500InternalServerError, ct);
//         }
//     }
// }
