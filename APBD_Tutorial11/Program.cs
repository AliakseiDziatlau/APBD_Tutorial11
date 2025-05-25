using APBD_Tutorial11.Infrastructure.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddDatabaseService(builder.Configuration)
    .AddApplicationServices()
    .AddScopedServices()
    .AddControllers();

builder.Build()
    .AddSwaggerServices()
    .AddControllerMapping()
    .Run();
