using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using TaskoPhobia.Infrastructure.Auth;
using TaskoPhobia.Infrastructure.DAL;
using TaskoPhobia.Infrastructure.Security;

namespace TaskoPhobia.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration )
    {
        var section = configuration.GetSection("app");
        services.Configure<AppOptions>(options => {});
        services.AddHttpContextAccessor();        
        services.AddEndpointsApiExplorer()
            .AddSwaggerGen(swagger =>
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
                        new string[] { }
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
                
            })
            .AddPostgres(configuration)
            .AddSecurity()
            .AddAuth(configuration);

        return services;
    }

    public static WebApplication UseInfrastructure(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        
        
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();

        return app;
    }
}