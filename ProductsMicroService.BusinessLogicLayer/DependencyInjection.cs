
using Microsoft.Extensions.DependencyInjection;
using ProductsMicroService.BusinessLogicLayer.Mappers;

namespace ProductsMicroService.BusinessLogicLayer;
public static class DependencyInjection
{
    /// <summary>
    /// Adds the business logic layer services to the specified service collection.
    /// </summary>
    /// <param name="services">The service collection to which the business logic layer services will be added.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddBusinessLogicLayer(this IServiceCollection services)
    {
        // Register AutoMapper with the specified mapping profile assembly.
        // Here assembly is used to automatically discover all mapping profiles in the specified assembly means in the business logic layer assembly.
        services.AddAutoMapper(cfg => { }, typeof(ProductAddRequestToProductMappingProfile).Assembly);
        return services;
    }

}
