using APBD_Tutorial11.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace APBD_Tutorial11.Infrastructure.DependencyInjection;

public static class DatabaseServiceExtensions
{
    public static IServiceCollection AddDatabaseService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        
        return services;
    }
}