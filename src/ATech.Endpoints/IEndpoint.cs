using Microsoft.AspNetCore.Routing;

namespace ATech.Endpoints;

public interface IEndpoint
{
    void MapEndpoint(IEndpointRouteBuilder app);
}

