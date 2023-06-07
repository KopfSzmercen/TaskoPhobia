using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using TaskoPhobia.Application.Security;

namespace TaskoPhobia.Infrastructure.Security;

internal static class Extensions
{
    public static IServiceCollection AddSecurity(this IServiceCollection services)
    {
        services.AddSingleton<IPasswordHasher<object>, PasswordHasher<object>>()
            .AddSingleton<IPasswordManager, PasswordManager>();

        return services;
    }
}