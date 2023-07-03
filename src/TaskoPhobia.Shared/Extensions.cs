using Microsoft.Extensions.DependencyInjection;
using TaskoPhobia.Shared.Abstractions.Time;
using TaskoPhobia.Shared.Time;

namespace TaskoPhobia.Shared;

public static class Extensions
{
    public static IServiceCollection AddShared(this IServiceCollection services)
    {
        services.AddSingleton<IClock, Clock>();
        return services;
    }
}