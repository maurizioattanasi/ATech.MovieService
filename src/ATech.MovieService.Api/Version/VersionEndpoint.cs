using ATech.Endpoints;
using ATech.MovieService.Application.Version;

using MediatR;

namespace ATech.MovieService.Api.Version;

public class VersionEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("api/version", HandleAsync).WithTags("version").AllowAnonymous();
    }

    private static async Task<IResult> HandleAsync(IMediator mediator)
    {
        var version = await mediator.Send(new VersionQuery());

        return Results.Ok(version.ToResponse());
    }
}
