using ATech.MovieService.Api.Movies.Dto;
using ATech.MovieService.Application.Movies.Queries;

using ATech.Endpoints;

using MediatR;

namespace ATech.MovieService.Api.Movies.Get;

public class GetMovieEndpoint() : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("api/movies/{id}", HandleAsync)
            .WithTags("movie")
            .AllowAnonymous();
    }

    private static async Task<IResult> HandleAsync(IMediator mediator, ILogger<GetMovieEndpoint> logger, string id)
    {
        try
        {
            logger.LogInformation("Getting movie with ID: {Id}", id);

            var query = new GetMovieByIdQuery(id);

            var movie = await mediator.Send(query);

            if (movie is null)
            {
                return Results.NotFound();
            }

            return Results.Ok(new GetMovieResponse(MovieMapper.ToDto(movie)));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while getting movie.");
            return Results.BadRequest();
        }
    }
}

public record GetMovieRequest(string Id);

public record GetMovieResponse(MovieDto Movie);


