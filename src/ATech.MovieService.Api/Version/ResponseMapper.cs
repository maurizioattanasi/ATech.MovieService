using ATech.MovieService.Application.Version;

namespace ATech.MovieService.Api.Version;

internal static class ResponseMapper
{
    public static VersionResponse ToResponse(this VersionResult result) => new VersionResponse(result.Version);
}
