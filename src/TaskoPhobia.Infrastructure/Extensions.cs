using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using TaskoPhobia.Infrastructure.Auth;
using TaskoPhobia.Infrastructure.DAL;
using TaskoPhobia.Infrastructure.Exceptions;
using TaskoPhobia.Infrastructure.Security;

namespace TaskoPhobia.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration )
    {
        // #CR pusta linijka
        services.AddHttpContextAccessor();        
        services.AddEndpointsApiExplorer()
            .AddSwaggerGen(ConfigureSwagger)
            .AddPostgres(configuration)
            .AddSecurity()
            .AddExceptions()
            .AddAuth(configuration);

        return services;
    }

    private static void ConfigureSwagger( SwaggerGenOptions swagger)
    {
        swagger.EnableAnnotations();
        swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                Array.Empty<string>()
            }
        });
        
        swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Description = "Input your JWT Authorization header to access this API. Example: \"Authorization: Bearer {token}\"",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer"
        });
    }

    public static WebApplication UseInfrastructure(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseMiddleware<ExceptionMiddleware>();
        
        app.UseAuthentication();
        app.UseAuthorization();
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