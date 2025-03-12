using eCommerceApp.Domain.Entities;
using eCommerceApp.Domain.Interfaces;
using eCommerceApp.Infrastructure.Data;
using eCommerceApp.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace eCommerceApp.Infrastructure.DependencyInjection;

/// <summary>
/// Provides extension methods for setting up infrastructure services.
/// </summary>
public static class ServiceContainer
{

    /// <summary>
    /// Adds infrastructure services to the specified <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The service collection to add services to.</param>
    /// <param name="configuration">The configuration to use for setting up services.</param>
    /// <returns>The service collection with the added services.</returns>
    public static IServiceCollection AddInfrastructureService
        (this IServiceCollection services, IConfiguration configuration)
    {
        // Configure the database context
        var connectionString = "Default";
        services.AddDbContext<AppDbContext>(option =>
        option.UseSqlServer(configuration.GetConnectionString(connectionString),
        sqlOptions =>
        {
            // Ensure this is the correct assembly
            sqlOptions.MigrationsAssembly
            (typeof(AppContext).Assembly.FullName);

            // Enable automatic retries for transient failure
            sqlOptions.EnableRetryOnFailure();
        }),
        ServiceLifetime.Scoped);

        // Register generic repositories
        services.AddScoped<IGeneric<Product>, GenericRepository<Product>>();
        services.AddScoped<IGeneric<Category>, GenericRepository<Category>>();

        return services;
    }
}
