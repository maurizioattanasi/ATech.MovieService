using System.Threading.Tasks;

using ATech.Endpoints;
using ATech.MovieService.Application.Version;

using MediatR;

using Microsoft.AspNetCore.Builder;

using Microsoft.AspNetCore.Http;

using Microsoft.AspNetCore.Routing;

namespace ATech.MovieService.Api.Version;

internal sealed class VersionEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("api/version", HandleAsync).WithTags("version").AllowAnonymous();
    }

    private static async Task<IResult> HandleAsync(IMediator mediator)
    {
        var version = await mediator.Send(new VersionQuery()).ConfigureAwait(false);

        return Results.Ok(version.ToResponse());
    }
}
