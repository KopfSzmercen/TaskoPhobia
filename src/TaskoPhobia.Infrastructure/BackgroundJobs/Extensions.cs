using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace TaskoPhobia.Infrastructure.BackgroundJobs;

internal static class Extensions
{
    internal static IServiceCollection AddBackgroundJobs(this IServiceCollection services)
    {
        services.AddQuartz(q =>
        {
            var processOutboxMessagesJobKey = new JobKey(nameof(ProcessOutboxMessagesJob));
            q.AddJob<ProcessOutboxMessagesJob>(processOutboxMessagesJobKey)
                .AddTrigger(t => t.ForJob(processOutboxMessagesJobKey)
                    .WithSimpleSchedule(s => s.WithIntervalInSeconds(15).RepeatForever())
                );
        });

        services.AddQuartzHostedService();
        return services;
    }
}