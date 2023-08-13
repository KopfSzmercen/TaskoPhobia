using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskoPhobia.Shared.Abstractions.Emails;

namespace TaskoPhobia.Infrastructure.Emails;

internal static class Extensions
{
    private const string SectionName = "emails";

    public static IServiceCollection AddEmails(this IServiceCollection services, IConfiguration configuration)
    {
        var section = configuration.GetSection(SectionName);
        services.Configure<EmailsOptions>(section);

        var options = configuration.GetOptions<EmailsOptions>(SectionName);
        services.AddScoped<IEmailSender, EmailSender>(x => new EmailSender(options));

        return services;
    }
}