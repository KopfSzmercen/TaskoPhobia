using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskoPhobia.Infrastructure.Auth;
using TaskoPhobia.Infrastructure.BackgroundJobs;
using TaskoPhobia.Infrastructure.DAL;
using TaskoPhobia.Infrastructure.Events;
using TaskoPhobia.Infrastructure.Exceptions;
using TaskoPhobia.Infrastructure.Security;
using TaskoPhobia.Infrastructure.Swagger;
using TaskoPhobia.Shared.Contexts;

namespace TaskoPhobia.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpContextAccessor();
        services.AddEndpointsApiExplorer()
            .AddSwaggerGen(ConfigureSwagger.ConfigureSwaggerOptions)
            .AddDomainEventsDispatching()
            .AddPostgres(configuration)
            .AddBackgroundJobs()
            .AddSecurity()
            .AddExceptions()
            .AddContext()
            .AddAuth(configuration);

        return services;
    }


    public static WebApplication UseInfrastructure(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseMiddleware<ExceptionMiddleware>();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseContext();
        app.MapControllers();

        return app;
    }

    public static T GetOptions<T>(this IConfiguration configuration, string sectionName) where T : class, new()
    {
        var options = new T();
        var section = configuration.GetSection(sectionName);
        section.Bind(options);

        return options;
    }
}