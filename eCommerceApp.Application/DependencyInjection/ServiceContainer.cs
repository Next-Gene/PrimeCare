using eCommerceApp.Application.Mapping;
using eCommerceApp.Application.Services.Implementations;
using eCommerceApp.Application.Services.Implementations.Authentucation;
using eCommerceApp.Application.Services.Interfaces;
using eCommerceApp.Application.Services.Interfaces.Authentucation;
using eCommerceApp.Application.Validations;
using eCommerceApp.Application.Validations.Authentication;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace eCommerceApp.Application.DependencyInjection;

/// <summary>
/// Provides extension methods to register application services in the dependency injection container.
/// </summary>
public static class ServiceContainer
{
    /// <summary>
    /// Adds application services to the specified <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add services to.</param>
    /// <returns>The <see cref="IServiceCollection"/> with the added services.</returns>
    public static IServiceCollection AddApplicationService
        (this IServiceCollection services)
    {
        // Configure the AutoMapper
        services.AddAutoMapper(typeof(MappingConfiguration));

        // Register Services
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssemblyContaining<CreateUserValidator>();
        services.AddScoped<IValidationService, ValidationService>();
        services.AddScoped<IAuthenticationService, AuthenticationService>();

        return services;
    }
}
