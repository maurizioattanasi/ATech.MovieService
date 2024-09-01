using ATech.MovieService.Api.Movies.Dto;
using ATech.MovieService.Application.Movies.Queries;

using FastEndpoints;

using MediatR;

namespace ATech.MovieService.Api.Movies.GetAll;

public class GetAllMoviesEndpoint(IMediator mediator, ILogger<GetAllMoviesEndpoint> logger) : Endpoint<GetAllMoviesRequest, GetAllMoviesResponse>
{
    public override void Configure()
    {
        Get("api/movies");

        Options(x => x.WithTags("Movies"));

        AllowAnonymous(Http.GET);
    }

    public override async Task HandleAsync(GetAllMoviesRequest req, CancellationToken ct)
    {
        try
        {
            logger.LogInformation("Getting all movies.");

            var movies = await mediator.Send(new GetMoviesQuery(), ct);

            if (movies is null || movies.Count() == 0)
            {
                await SendNotFoundAsync(ct);
                return;
            }

            var dtos = movies
                .Select(m => MovieMapper.ToDto(m)).Take(10).ToArray();

            Response = new GetAllMoviesResponse(dtos);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while getting movies.");
        }
    }
}

public record GetAllMoviesRequest(uint Page, int PageSize);

public record GetAllMoviesResponse(MovieDto[] Movies);