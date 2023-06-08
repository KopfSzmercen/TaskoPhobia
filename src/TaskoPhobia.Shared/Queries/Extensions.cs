using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using TaskoPhobia.Shared.Abstractions.Commands;
using TaskoPhobia.Shared.Abstractions.Queries;
using TaskoPhobia.Shared.Commands;

namespace TaskoPhobia.Shared.Queries;

public static class Extensions
{
    public static IServiceCollection AddQueries(this IServiceCollection services)
    {

        var assembly = Assembly.GetCallingAssembly();
        
        services.AddSingleton<IQueryDispatcher, InMemoryQueryDispatcher>();
        
        services.Scan(s => s.FromAssemblies(assembly)
            .AddClasses(c => c.AssignableTo(typeof(IQueryHandler<,>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());
        
        return services;
    }
}