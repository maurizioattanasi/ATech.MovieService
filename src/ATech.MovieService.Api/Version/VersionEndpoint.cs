using ATech.MovieService.Application.Version;

using FastEndpoints;

using MediatR;

namespace ATech.MovieService.Api.Version;

public class VersionEndpoint(IMediator mediator) : Endpoint<EmptyRequest, VersionResponse, VersionResponseMapper>
{
    public override void Configure()
    {
        Get("api/Version");

        Options(opt=>opt.WithTags("Version"));

        AllowAnonymous(Http.GET);
    }

    public override async Task HandleAsync(EmptyRequest req, CancellationToken ct)
    {
        var response = await mediator.Send(new VersionQuery());

        Response = Map.FromEntity(response);
    }
}

public class VersionResponseMapper : Mapper<EmptyRequest, VersionResponse, VersionResult>
{
    public override VersionResponse FromEntity(VersionResult e) => new VersionResponse(e.Version);
}

public record VersionResponse(string Version);