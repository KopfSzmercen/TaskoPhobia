﻿using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using TaskoPhobia.Shared.Processing;

namespace TaskoPhobia.Shared.Events;

public static class Extensions
{
    public static IServiceCollection AddDomainEventHandlers(this IServiceCollection services)
    {
        var assembly = Assembly.GetCallingAssembly();
        services.Scan(s => s.FromAssemblies(assembly)
            .AddClasses(c => c.AssignableTo(typeof(IDomainEventHandler<>)))
            .AsSelfWithInterfaces()
            .WithScopedLifetime()
        );

        return services;
    }
}