using System.IO.Compression;

using ATech.Endpoints;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ATech.MovieService.Api;

internal static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddAuthorization()
            .AddCompression(configuration)
            .AddEndpoints(typeof(DependencyInjection).Assembly)
            // .SwaggerDocument(opt =>
            // {
            //     opt.DocumentSettings = s =>
            //     {
            //         s.Title = "ATech Movie Service Api";
            //         s.Version = "v1";
            //     };

            //     opt.AutoTagPathSegmentIndex = 0;
            // })
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
