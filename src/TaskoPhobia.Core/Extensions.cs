using Microsoft.Extensions.DependencyInjection;
using TaskoPhobia.Core.DomainServices;
using TaskoPhobia.Core.Policies.Payments;

namespace TaskoPhobia.Core;

public static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddDomainServices();
        services.AddPaymentPolicies();
        return services;
    }
}