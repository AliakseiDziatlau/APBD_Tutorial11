namespace APBD_Tutorial11.Infrastructure.DependencyInjection;

public static class ControllerMappingExtensions
{
    public static WebApplication AddControllerMapping(this WebApplication app)
    {
        app.MapControllers();
        return app;
    }
}