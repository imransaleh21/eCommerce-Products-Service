
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductsMicroService.DataAccessLayer.DBContext;
using ProductsMicroService.DataAccessLayer.Repository;
using ProductsMicroService.DataAccessLayer.RepositoryContracts;

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
        string formatConnectionString = configuration.GetConnectionString("MySqlConnection")!;
        string connectionStr = formatConnectionString.Replace("$MYSQL_HOST", Environment.GetEnvironmentVariable("MYSQL_HOST"))
        .Replace("$MYSQL_PASS", Environment.GetEnvironmentVariable("MYSQL_PASS"));

        services.AddDbContext<ApplicationDbContext>(options =>
        options.UseMySql(
                    connectionStr,
                    ServerVersion.AutoDetect(connectionStr),
                    mySqlOptions => mySqlOptions.EnableStringComparisonTranslations()
                ));
        services.AddScoped<IProductsRepository, ProductsRepository>();
        return services;
    }

}
