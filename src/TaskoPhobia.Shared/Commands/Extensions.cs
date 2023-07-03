using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using TaskoPhobia.Shared.Abstractions.Commands;

namespace TaskoPhobia.Shared.Commands;

public static class Extensions
{
    public static IServiceCollection AddCommands(this IServiceCollection services)
    {
        var assembly = Assembly.GetCallingAssembly();

        services.AddSingleton<ICommandDispatcher, InMemoryCommandDispatcher>();
        services.Scan(s => s.FromAssemblies(assembly)
            .AddClasses(c => c.AssignableTo(typeof(ICommandHandler<>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        services.Scan(s => s.FromAssemblies(assembly)
            .AddClasses(c => c.AssignableTo(typeof(IValidator<>)))
            .AsSelfWithInterfaces()
            .WithScopedLifetime());


        return services;
    }
}