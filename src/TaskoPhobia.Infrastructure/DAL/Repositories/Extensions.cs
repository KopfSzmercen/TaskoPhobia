using Microsoft.Extensions.DependencyInjection;
using TaskoPhobia.Core.Repositories;

namespace TaskoPhobia.Infrastructure.DAL.Repositories;

internal static  class Extensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, PostgresUserRepository>();
        services.AddScoped<IProjectRepository, PostgresProjectRepository>();
        return services;
    }
}