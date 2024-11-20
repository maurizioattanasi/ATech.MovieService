using System;
using System.IO.Compression;

using FastEndpoints;
using FastEndpoints.Swagger;

using Microsoft.AspNetCore.ResponseCompression;

namespace ATech.MovieService.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddAuthorization()
            .AddFastEndpoints()
            .AddCompression(configuration)
            .SwaggerDocument(opt =>
            {
                opt.DocumentSettings = s =>
                {
                    s.Title = "ATech Movie Service Api";
                    s.Version = "v1";
                };

                opt.AutoTagPathSegmentIndex = 0;
            })
            ;

        return services;
    }

    private static IServiceCollection AddCompression(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddResponseCompression(options =>
            {
                options.EnableForHttps = true;
                options.Providers.Add<BrotliCompressionProvider>();
                options.Providers.Add<GzipCompressionProvider>();
            });

        services
            .Configure<BrotliCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Fastest;
            })
            .Configure<GzipCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.SmallestSize;
            });
            

        return services;
    }
}
