using Microsoft.Extensions.DependencyInjection;
using TaskoPhobia.Application.Commands.Payments.CreatePaymentLink;

namespace TaskoPhobia.Infrastructure.HttpContextStorage;

internal static class Extensions
{
    public static IServiceCollection AddHttpContextStorage(this IServiceCollection services)
    {
        services.AddSingleton<IPaymentLinkStorage, HttpContextPaymentLinkStorage>();
        return services;
    }
}