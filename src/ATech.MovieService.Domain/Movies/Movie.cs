namespace ATech.MovieService.Domain.Movies;

public class Movie
{
    public string Id { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string Rated { get; set; } = null!;

    public string Plot { get; set; } = null!;
}
