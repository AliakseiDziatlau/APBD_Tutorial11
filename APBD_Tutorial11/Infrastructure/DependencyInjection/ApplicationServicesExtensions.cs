using APBD_Tutorial11.Application.Mapping;

namespace APBD_Tutorial11.Infrastructure.DependencyInjection;

public static class ApplicationServicesExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
        
        services.AddAutoMapper(typeof(MappingProfile));
        
        return services;
    }
}