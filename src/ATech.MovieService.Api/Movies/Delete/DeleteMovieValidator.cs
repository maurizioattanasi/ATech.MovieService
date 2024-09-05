using FastEndpoints;

using FluentValidation;

namespace ATech.MovieService.Api.Movies.Delete;

public class DeleteMovieValidator : Validator<DeleteMovieRequest>
{
    public DeleteMovieValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Movie ID is required.");
    }
}
