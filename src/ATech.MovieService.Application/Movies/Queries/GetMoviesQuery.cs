using ATech.MovieService.Domain.Movies;
using ATech.Pagination;

using MediatR;

namespace ATech.MovieService.Application.Movies.Queries;

public record GetMoviesQuery(PagingParameters PagingParameters) : IRequest<PagedList<Movie>>;

