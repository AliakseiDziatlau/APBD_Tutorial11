using APBD_Tutorial11.Domain.Interfaces;
using APBD_Tutorial11.Infrastructure.Repositories;

namespace APBD_Tutorial11.Infrastructure.DependencyInjection;

public static class ScopedServicesExtensions
{
    public static IServiceCollection AddScopedServices(this IServiceCollection services)
    {
        services.AddScoped<IDoctorRepository, DoctorRepository>();
        services.AddScoped<IPatientRepository, PatientRepository>();
        services.AddScoped<IMedicamentRepository, MedicamentRepository>();
        services.AddScoped<IPrescriptionRepository, PrescriptionRepository>();
        return services;
    }
}