using ATech.MovieService.Domain.Movies;

using FastEndpoints;

using MediatR;

namespace ATech.MovieService.Api.Movies.Get;

public class GetMovieEndpoint(IMediator mediator, ILogger<GetMovieEndpoint> logger) : Endpoint<GetMovieRequest, GetMovieResponse>
{
    public override void Configure()
    {
        Get("api/movies");

        Options(x => x.WithTags("Movies"));

        AllowAnonymous(Http.GET);
    }

    public override async Task HandleAsync(GetMovieRequest req, CancellationToken ct)
    {
        try
        {
            logger.LogInformation("Getting movie with ID: {Id}", req.Id);
            await Task.Yield();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while getting movie.");
        }
    }
}

public record GetMovieRequest(string Id);

public record GetMovieResponse(MovieDto Movie);

public record MovieDto(string Id, string Title, string Rated, string Plot);

public static class MovieMapper
{
    public static MovieDto ToDto(Movie movie) => new MovieDto(movie.Id, movie.Title, movie.Rated, movie.Plot);
}
