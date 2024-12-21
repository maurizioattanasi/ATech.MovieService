using ATech.Endpoints;
using ATech.MovieService.Api.Movies.Dto;
using ATech.MovieService.Application.Movies.Commands;

using MediatR;


namespace ATech.MovieService.Api.Movies.Create;

public class CreateMovieEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("api/movies", HandleAsync)
            .WithTags("movies")
            .AllowAnonymous();
    }

    private static async Task<IResult> HandleAsync(IMediator mediator, ILogger<CreateMovieEndpoint> logger, CreateMovieRequest request, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            var command = new CreateMovieCommand(request.Movie.Title, request.Movie.Rated, request.Movie.Plot);

            var movie = await mediator.Send(command, cancellationToken);

            if (movie is null)
            {
                return Results.NotFound();
            }

            return Results.Ok(MovieMapper.ToDto(movie));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while creating a new movie.");

            return Results.BadRequest();
        }
    }
}

// public class CreateMovieEndpoint(IMediator mediator, ILogger<CreateMovieEndpoint> logger)
//     : Endpoint<CreateMovieRequest, CreateMovieResponse>
// {
//     public override void Configure()
//     {
//         Post("api/movies");

//         Options(x => x.WithTags("Movies"));

//         AllowAnonymous(Http.POST);
//     }

//     public override async Task HandleAsync(CreateMovieRequest req, CancellationToken ct)
//     {
//         try
//         {
//             var command = new CreateMovieCommand(req.Movie.Title, req.Movie.Rated, req.Movie.Plot);

//             var movie = await mediator.Send(command, ct);

//             if (movie is null)
//             {
//                 await SendErrorsAsync(StatusCodes.Status400BadRequest, ct);
//                 return;
//             }

//             Response = new CreateMovieResponse(MovieMapper.ToDto(movie));
//         }
//         catch (Exception ex)
//         {
//             logger.LogError(ex, "An error occurred while creating a new movie.");

//             await SendErrorsAsync(StatusCodes.Status500InternalServerError, ct);
//         }
//     }
// }
