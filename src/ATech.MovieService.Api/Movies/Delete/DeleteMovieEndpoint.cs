// using ATech.MovieService.Application.Common.Exceptions;
// using ATech.MovieService.Application.Movies.Commands;

// using FastEndpoints;

// using MediatR;

// namespace ATech.MovieService.Api.Movies.Delete;

// public class DeleteMovieEndpoint(IMediator mediator, ILogger<DeleteMovieEndpoint> logger) : Endpoint<DeleteMovieRequest, EmptyResponse>
// {
//     public override void Configure()
//     {
//         Delete("api/movies/{id}");

//         Options(x => x.WithTags("Movies"));

//         AllowAnonymous(Http.DELETE);
//     }

//     public override async Task HandleAsync(DeleteMovieRequest req, CancellationToken ct)
//     {
//         try
//         {
//             await mediator.Send(new DeleteMovieCommand(req.Id), ct);

//             Response = new EmptyResponse();
//         }
//         catch (ItemNotFoundException)
//         {
//             await SendNotFoundAsync(ct);
//         }
//         catch (Exception ex)
//         {
//             // Handle exception
//             logger.LogError(ex, "An error occurred while deleting a movie.");

//             // Return HTTP 500 Internal Server Error
//             await SendErrorsAsync(StatusCodes.Status500InternalServerError, ct);
//         }
//     }
// }
