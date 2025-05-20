using System;
using System.Threading;
using System.Threading.Tasks;

using ATech.Endpoints;
using ATech.MovieService.Api.Movies.Dto;
using ATech.MovieService.Application.Movies.Commands;

using MediatR;

using Microsoft.AspNetCore.Builder;

using Microsoft.AspNetCore.Http;

using Microsoft.AspNetCore.Routing;

using Microsoft.Extensions.Logging;


namespace ATech.MovieService.Api.Movies.Create;

internal sealed class CreateMovieEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("api/movies", HandleAsync)
            .WithTags("movies")
            .AllowAnonymous();
    }

    private static async Task<IResult> HandleAsync(IMediator mediator, ILogger<CreateMovieEndpoint> logger, CreateMovieRequest request, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            var command = new CreateMovieCommand(request.Movie.Title, request.Movie.Rated, request.Movie.Plot);

            var movie = await mediator.Send(command, cancellationToken).ConfigureAwait(false);

            if (movie is null)
            {
                return Results.NotFound();
            }

            return Results.Ok(MovieMapper.ToDto(movie));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while creating a new movie.");

            return Results.BadRequest();
        }
    }
}