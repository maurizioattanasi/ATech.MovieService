using FastEndpoints;

using FluentValidation;

namespace ATech.MovieService.Api.Movies.Create;

public class CreateMovieRequestValidator : Validator<CreateMovieRequest> { 
    public CreateMovieRequestValidator()
    {
        RuleFor(x => x.Movie)
            .NotNull()
            .WithMessage("Movie details are required.");

        RuleFor(x => x.Movie.Title)
            .NotEmpty()
            .MaximumLength(100)
            .WithMessage("Title must not be empty and maximum length is 100 characters.");
    }
}
