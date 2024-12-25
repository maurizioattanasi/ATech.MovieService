using ATech.MovieService.Application.Movies.Interfaces;
using ATech.MovieService.Infrastructure.Common.Persistence;
using ATech.MovieService.Infrastructure.Movies;
using ATech.Repository;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ATech.MovieService.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services
           .AddPersistence(configuration);

        return services;
    }

    private static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("AppContext") ?? "";

        var dbSettings = configuration.GetSection(nameof(DataBaseSettings)).Get<DataBaseSettings>();

        if (dbSettings is null)
        {
            throw new InvalidOperationException("No 'DataBaseSettings' section found in the configuration.");
        }

        services
            .AddDbContext<AppDbContext>(options => options.UseMongoDB(connectionString, dbSettings.DataBaseName));

        services.AddScoped<IMovieRepository, MovieRepository>();

        return services;
    }

}
