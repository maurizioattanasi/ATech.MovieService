using MediatR;

namespace ATech.MovieService.Application.Version;

public record VersionQuery : IRequest<VersionResult>;
