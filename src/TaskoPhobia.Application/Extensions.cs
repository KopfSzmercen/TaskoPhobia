using Microsoft.Extensions.DependencyInjection;
using TaskoPhobia.Shared.Commands;
using TaskoPhobia.Shared.Events;

namespace TaskoPhobia.Application;

public static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddCommands();
        services.AddDomainEventHandlers();
        return services;
    }
}