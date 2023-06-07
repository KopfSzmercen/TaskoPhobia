using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskoPhobia.Infrastructure.DAL;
using TaskoPhobia.Infrastructure.Security;

namespace TaskoPhobia.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration )
    {
        var section = configuration.GetSection("app");
        services.Configure<AppOptions>(options => {});
        
        services.AddEndpointsApiExplorer()
            .AddSwaggerGen()
            .AddPostgres(configuration)
            .AddSecurity();

        return services;
    }

    public static WebApplication UseInfrastructure(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();

        app.MapControllers();

        return app;
    }
}