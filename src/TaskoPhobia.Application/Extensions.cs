using Microsoft.Extensions.DependencyInjection;
using TaskoPhobia.Shared.Commands;

namespace TaskoPhobia.Application;

public static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddCommands();
        return services;
    }
}