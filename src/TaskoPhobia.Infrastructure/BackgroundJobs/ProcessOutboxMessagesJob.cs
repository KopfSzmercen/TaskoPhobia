using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Quartz;
using TaskoPhobia.Infrastructure.DAL.Contexts;
using TaskoPhobia.Shared.Abstractions.Domain;
using TaskoPhobia.Shared.Abstractions.Outbox;
using TaskoPhobia.Shared.Events;

namespace TaskoPhobia.Infrastructure.BackgroundJobs;

[DisallowConcurrentExecution]
internal sealed class ProcessOutboxMessagesJob : IJob
{
    private readonly TaskoPhobiaWriteDbContext _dbContext;
    private readonly DbSet<OutboxMessage> _outboxMessages;
    private readonly IServiceProvider _serviceProvider;

    public ProcessOutboxMessagesJob(TaskoPhobiaWriteDbContext dbContext, IServiceProvider serviceProvider)
    {
        _outboxMessages = dbContext.OutboxMessages;
        _dbContext = dbContext;
        _serviceProvider = serviceProvider;
    }

    [SuppressMessage("ReSharper.DPA", "DPA0006: Large number of DB commands", MessageId = "count: 75")]
    public async Task Execute(IJobExecutionContext context)
    {
        var messages = await _outboxMessages
            .Where(m => m.ProcessedDate == null)
            .OrderBy(x => x.OccurredOn)
            .Take(10)
            .ToListAsync();


        using var scope = _serviceProvider.CreateScope();
        foreach (var outboxMessage in messages)
        {
            var domainNotification = JsonConvert
                .DeserializeObject<IDomainEventNotification<IDomainEvent>>(outboxMessage.Data,
                    new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.Auto
                    });

            if (domainNotification is null) continue;

            var handlerType =
                typeof(IDomainNotificationHandler<>).MakeGenericType(domainNotification.DomainEvent.GetType());
            var handlers = scope.ServiceProvider.GetServices(handlerType);

            var tasks = handlers.Select(x =>
                (Task)handlerType
                    .GetMethod(nameof(IDomainNotificationHandler<IDomainEvent>.HandleAsync))
                    ?.Invoke(x, new object[] { domainNotification.DomainEvent }));

            await Task.WhenAll(tasks);

            outboxMessage.ProcessedDate = DateTimeOffset.Now;
        }

        await _dbContext.SaveChangesAsync();
    }
}