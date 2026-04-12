
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductsMicroService.DataAccessLayer.DBContext;

namespace ProductsMicroService.DataAccessLayer;

public static class DependencyInjection
{
    /// <summary>
    /// Adds data access layer services to the specified service collection.
    /// </summary>
    /// <param name="services">The service collection to which the data access layer services are added.</param>
    /// <returns>The same service collection instance, to allow for method chaining.</returns>
    public static IServiceCollection AddDataAccessLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        options.UseMySql(
                    configuration.GetConnectionString("MySqlConnection"),
                    ServerVersion.AutoDetect(configuration.GetConnectionString("MySqlConnection"))
                ));
        return services;
    }

}
