using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskoPhobia.Shared.Abstractions.Payments;

namespace TaskoPhobia.Infrastructure.Payments;

internal static class Extensions
{
    private const string SectionName = "Payments";

    public static IServiceCollection AddPayments(this IServiceCollection services, IConfiguration configuration)
    {
        var section = configuration.GetSection(SectionName);
        services.Configure<PaymentOptions>(section);

        var options = configuration.GetOptions<PaymentOptions>(SectionName);
        services.AddScoped<IPaymentProcessor, PaymentProcessor>(x => new PaymentProcessor(options));

        return services;
    }
}