using Microsoft.Extensions.DependencyInjection;

namespace TaskoPhobia.Infrastructure.Exceptions;

internal static class Extensions
{
    public static IServiceCollection AddExceptions(this IServiceCollection services)
    {
        services.AddSingleton<ExceptionMiddleware>();
        return services;
    }
}