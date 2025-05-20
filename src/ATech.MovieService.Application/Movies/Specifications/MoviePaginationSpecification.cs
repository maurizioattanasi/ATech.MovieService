using ATech.MovieService.Domain.Movies;
using ATech.Repository;

namespace ATech.MovieService.Application.Movies.Specifications;

public class MoviePaginationSpecification : Specification<Movie>
{
    public MoviePaginationSpecification(int? pageIndex = null, int? pageSize = null) : base(skip: ((pageIndex ?? 0) - 1) * pageSize ?? 0, take: pageSize)
    {
        AddOrderBy(x => x.Title ?? string.Empty);
    }

}