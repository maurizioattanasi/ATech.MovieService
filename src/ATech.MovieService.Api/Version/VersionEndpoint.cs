using ATech.Endpoints;
using ATech.MovieService.Application.Version;

using MediatR;


namespace ATech.MovieService.Api.Version;

public class VersionEndpoint(IMediator mediator) : IEndpoint
{
    public VersionResponse Result { get; set; } = null!;

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("api/Version", () => ExecuteAsync());
    }

    private async Task<IResult> ExecuteAsync()
    {
        await HandleAsync();

        return Results.Ok(new VersionResponse("1.0.0"));
    }

    public async Task HandleAsync()
    {
        var version = await mediator.Send(new VersionQuery());

        Result = version.ToResponse();
    }
}

public static class ResponseMapper
{
    public static VersionResponse ToResponse(this VersionResult result) => new VersionResponse(result.Version);
}

// public class VersionEndpoint(IMediator mediator) : Endpoint<EmptyRequest, VersionResponse, VersionResponseMapper>
// {
//     public override void Configure()
//     {
//         Get("api/Version");

//         Options(opt => opt.WithTags("Version"));

//         AllowAnonymous(Http.GET);
//     }

//     public override async Task HandleAsync(EmptyRequest req, CancellationToken ct)
//     {
//         var response = await mediator.Send(new VersionQuery());

//         Response = Map.FromEntity(response);
//     }
// }

// public class VersionResponseMapper : Mapper<EmptyRequest, VersionResponse, VersionResult>
// {
//     public override VersionResponse FromEntity(VersionResult e) => new VersionResponse(e.Version);
// }

public record VersionResponse(string Version);