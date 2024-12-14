// using ATech.MovieService.Api.Movies.Dto;
// using ATech.MovieService.Application.Movies.Queries;
// using ATech.MovieService.Domain.Movies;
// using ATech.Pagination;

// using FastEndpoints;

// using MediatR;

// using Newtonsoft.Json;

// namespace ATech.MovieService.Api.Movies.GetAll;

// public class GetAllMoviesEndpoint(IMediator mediator, ILogger<GetAllMoviesEndpoint> logger) : Endpoint<GetAllMoviesRequest, GetAllMoviesResponse>
// {
//     public override void Configure()
//     {
//         Get("api/movies");

//         Options(x => x.WithTags("Movies"));

//         AllowAnonymous(Http.GET);
//     }

//     public override async Task HandleAsync(GetAllMoviesRequest req, CancellationToken ct)
//     {
//         try
//         {
//             logger.LogInformation("Getting all movies.");

//             var movies = await mediator.Send(new GetMoviesQuery(new PagingParameters { PageNumber = req.PageNumber, PageSize = req.PageSize }), ct);

//             if (movies is null || movies.Count() == 0)
//             {
//                 await SendNotFoundAsync(ct);
//                 return;
//             }

//             var dtos = movies.Select(m => MovieMapper.ToDto(m));
            
//             Response = new GetAllMoviesResponse(new PagedList<MovieDto>(dtos.ToList(), movies.TotalCount, movies.CurrentPage, movies.PageSize));
//         }
//         catch (Exception ex)
//         {
//             logger.LogError(ex, "An error occurred while getting movies.");
//         }
//     }
// }

// public record GetAllMoviesRequest(int PageNumber = 1, int PageSize = 10);

// public record GetAllMoviesResponse(PagedList<MovieDto> Movies)
// {
//     [ToHeader("x-pagination")]
//     public string PaginationMetadata => JsonConvert.SerializeObject(Movies.PaginationMetadata);
// }
