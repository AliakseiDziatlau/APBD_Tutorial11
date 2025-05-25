namespace APBD_Tutorial11.Infrastructure.DependencyInjection;

public static class SwaggerApplicationExtensions
{
    public static WebApplication AddSwaggerServices(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        
        return app;
    }
}