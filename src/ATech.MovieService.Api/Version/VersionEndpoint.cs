using ATech.Endpoints;
using ATech.MovieService.Application.Version;

using MediatR;

namespace ATech.MovieService.Api.Version;

public class VersionEndpoint(IMediator mediator) : Endpoint<VersionResponse>
{
    public override void Configure()
    {
        Get("api/Version");
    }

    public override async Task HandleAsync()
    {
        var version = await mediator.Send(new VersionQuery());

        Response = version.ToResponse();
    }
}
