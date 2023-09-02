using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using TaskoPhobia.Core.Policies.Payments.SetPaymentStatusPolicies;

namespace TaskoPhobia.Core.Policies.Payments;

internal static class Extensions
{
    public static IServiceCollection AddPaymentPolicies(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();

        services.Scan(s => s.FromAssemblies(assembly)
            .AddClasses(c => c.AssignableTo(typeof(ISetPaymentStatusPolicy)))
            .AsImplementedInterfaces()
            .WithSingletonLifetime()
        );

        return services;
    }
}