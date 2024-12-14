using ATech.MovieService.Application.Version;

namespace ATech.MovieService.Api.Version;

public static class ResponseMapper
{
    public static VersionResponse ToResponse(this VersionResult result) => new VersionResponse(result.Version);
}
