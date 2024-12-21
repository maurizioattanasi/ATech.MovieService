using ATech.Endpoints;
using ATech.MovieService.Api.Movies.Dto;
using ATech.Pagination;
using ATech.MovieService.Application.Movies.Queries;

using MediatR;
using Newtonsoft.Json;

namespace ATech.MovieService.Api.Movies.GetAll;

public class GetAllMoviesEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("api/movies", HandleAsync)
            .WithTags("movies")
            .AllowAnonymous();
    }

    private static async Task<IResult> HandleAsync(IMediator mediator, HttpContext context, ILogger<GetAllMoviesEndpoint> logger, int PageNumber = 1, int PageSize = 10)
    {
        try
        {
            logger.LogInformation("Getting all movies.");

            var movies = await mediator.Send(new GetMoviesQuery(new PagingParameters { PageNumber = PageNumber, PageSize = PageSize }));

            if (movies is null || movies.Count() == 0)
            {
                return Results.NotFound();
            }

            var dtos = movies.Select(m => MovieMapper.ToDto(m));
            var response = new GetAllMoviesResponse(new PagedList<MovieDto>(dtos.ToList(), movies.TotalCount, movies.CurrentPage, movies.PageSize));

            var result = Results.Ok(response);
            context.Response.Headers["x-pagination"] = response.PaginationMetadata;

            return Results.Ok(response.Movies);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while getting movies.");
            return Results.BadRequest();
        }
    }
}

public record GetAllMoviesRequest(int PageNumber = 1, int PageSize = 10);

public record GetAllMoviesResponse(PagedList<MovieDto> Movies)
{
    // [ToHeader("x-pagination")]
    public string PaginationMetadata => JsonConvert.SerializeObject(Movies.PaginationMetadata);
}
