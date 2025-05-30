using System;
using System.Threading;
using System.Threading.Tasks;

using ATech.Endpoints;
using ATech.MovieService.Api.Movies.Dto;
using ATech.MovieService.Application.Common.Exceptions;
using ATech.MovieService.Application.Movies.Commands;

using MediatR;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;


namespace ATech.MovieService.Api.Movies.Update;

internal sealed class UpdateMovieEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPatch("api/movies/{id}", HandleAsync).WithTags("movies").AllowAnonymous();
    }

    public static async Task<IResult> HandleAsync(IMediator mediator,
                                                  ILogger<UpdateMovieEndpoint> logger,
                                                  string id,
                                                  UpdateMovieRequest request,
                                                  CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            var command = new UpdateMovieCommand(id, request.Movie.Title, request.Movie.Rated, request.Movie.Plot);

            var updatedMovie = await mediator.Send(command, cancellationToken).ConfigureAwait(false);

            if (updatedMovie is null)
            {
                return Results.NoContent();
            }

            return Results.Ok(new UpdateMovieRequestResponse(MovieMapper.ToDto(updatedMovie)));
        }
        catch (ItemNotFoundException)
        {
            return Results.NotFound();
        }
        catch (Exception ex)
        {
            // Handle exception
            logger.LogError(ex, "An error occurred while updating a movie.");

            // Return HTTP 500 Internal Server Error
            return Results.BadRequest();
        }
    }
}