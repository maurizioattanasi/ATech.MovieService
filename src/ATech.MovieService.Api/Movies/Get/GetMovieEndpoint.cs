using ATech.MovieService.Api.Movies.Dto;
using ATech.MovieService.Application.Movies.Queries;

using FastEndpoints;

using MediatR;

namespace ATech.MovieService.Api.Movies.Get;

public class GetMovieEndpoint(IMediator mediator, ILogger<GetMovieEndpoint> logger) : Endpoint<GetMovieRequest, GetMovieResponse>
{
    public override void Configure()
    {
        Get("api/movies/{id}");

        Options(x => x.WithTags("Movies"));

        AllowAnonymous(Http.GET);
    }

    public override async Task HandleAsync(GetMovieRequest req, CancellationToken ct)
    {
        try
        {
            logger.LogInformation("Getting movie with ID: {Id}", req.Id);

            var query = new GetMovieByIdQuery(req.Id);

            var movie = await mediator.Send(query, ct);

            if (movie is null)
            {
                await SendNotFoundAsync(ct);
                return;
            }

            Response = new GetMovieResponse(MovieMapper.ToDto(movie));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while getting movie.");
            await SendErrorsAsync(StatusCodes.Status500InternalServerError, ct);
        }
    }
}

public record GetMovieRequest(string Id);

public record GetMovieResponse(MovieDto Movie);
