using eCommerceApp.Application.Services.Interfaces.Logging;
using eCommerceApp.Domain.Entities;
using eCommerceApp.Domain.Interfaces;
using eCommerceApp.Infrastructure.Data;
using eCommerceApp.Infrastructure.Middleware;
using eCommerceApp.Infrastructure.Repositories;
using eCommerceApp.Infrastructure.Services;
using EntityFramework.Exceptions.SqlServer;
using Microsoft.AspNetCore.Builder;
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
            (typeof(AppDbContext).Assembly.FullName);

            // Enable automatic retries for transient failure
            sqlOptions.EnableRetryOnFailure();
        })
        .UseExceptionProcessor(), ServiceLifetime.Scoped);

        // Register generic repositories
        services.AddScoped<IGeneric<Product>, GenericRepository<Product>>();
        services.AddScoped<IGeneric<Category>, GenericRepository<Category>>();

        // Register generic Logger
        services.AddScoped(typeof(IAppLogger<>), typeof(SerilogLoggerAdapter<>));

        return services;
    }

    /// <summary>
    /// Configures the application to use infrastructure services.
    /// </summary>
    /// <param name="app">The application builder to configure.</param>
    /// <returns>The application builder with the configured services.</returns>
    public static IApplicationBuilder UseInfrastructureService
        (this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionHandlingMiddleware>();

        return app;
    }
}
