using ATech.MovieService.Application.Movies.Interfaces;
using ATech.MovieService.Application.Movies.Queries;
using ATech.MovieService.Application.Movies.Specifications;
using ATech.MovieService.Domain.Movies;
using ATech.Pagination;

using MediatR;

using Microsoft.Extensions.Logging;

namespace ATech.MovieService.Application.Movies.Handlers;

public class GetMoviesHandler(IMovieRepository repository, ILogger<GetMoviesHandler> logger) : IRequestHandler<GetMoviesQuery, PagedList<Movie>>
{
    public async Task<PagedList<Movie>> Handle(GetMoviesQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Returning movies from {@PageNumber} for {@PageSize}...", request.PagingParameters.PageNumber, request.PagingParameters.PageSize);

#if true
        var count = await repository.CountAsync(cancellationToken);
        var movies = await repository.ListAsync(new MoviePaginationSpecification(request.PagingParameters.PageNumber, request.PagingParameters.PageSize), cancellationToken);

        return new PagedList<Movie>(movies, count, request.PagingParameters.PageNumber, request.PagingParameters.PageSize);
#else
        var movies = await repository.GetAllAsync(cancellationToken);

        return PagedList<Movie>.ToPagedList(movies.AsQueryable(), request.PagingParameters.PageNumber, request.PagingParameters.PageSize);
#endif
    }

}

