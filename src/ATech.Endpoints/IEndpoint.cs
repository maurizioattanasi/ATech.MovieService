using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace ATech.Endpoints;

public interface IEndpoint
{
    void MapEndpoint(IEndpointRouteBuilder app);
}

public abstract class Endpoint<TResponse> : IEndpoint where TResponse : notnull
{
    private IEndpointRouteBuilder _app = null!;

    public TResponse Response { get; set; } = default!;

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        _app = app;

        Configure();
    }

    public virtual void Configure() => throw new NotImplementedException();

    public abstract Task HandleAsync();

    public void Get(string routePattern)
    {
        _app.MapGet(routePattern, () => ExecuteAsync());
    }

    private async Task<IResult> ExecuteAsync()
    {
        await HandleAsync();

        return Results.Ok(Response);
    }
}
