using System.ComponentModel.DataAnnotations.Schema;

namespace ATech.MovieService.Domain.Movies;

[Table("movies")]
public class Movie
{
    public string Id { get; set; } = null!;

    [Column("title")]
    public string? Title { get; set; }

    [Column("rated")]
    public string? Rated { get; set; }

    [Column("plot")]
    public string? Plot { get; set; }
}
