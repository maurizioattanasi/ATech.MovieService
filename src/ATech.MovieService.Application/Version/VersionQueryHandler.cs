using System;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Microsoft.Extensions.Logging;

namespace ATech.MovieService.Application.Version;

public class VersionQueryHandler(ILogger<VersionQueryHandler> logger) : IRequestHandler<VersionQuery, VersionResult>
{
    public async Task<VersionResult> Handle(VersionQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Retrieving Assembly Version...");

        var assemblyName = System.Reflection.Assembly.GetEntryAssembly()?.GetName();

        if (assemblyName is null)
        {
            throw new InvalidProgramException("Cannot retrieve the entry assembly name...");
        }

        var version = assemblyName.Version?.ToString() ?? string.Empty;

        return await Task.FromResult(new VersionResult(version)).ConfigureAwait(false);
    }
}
