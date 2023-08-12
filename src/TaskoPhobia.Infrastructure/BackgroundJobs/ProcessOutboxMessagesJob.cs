using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Quartz;
using TaskoPhobia.Infrastructure.DAL.Contexts;
using TaskoPhobia.Shared.Abstractions.Outbox;
using TaskoPhobia.Shared.Events;

namespace TaskoPhobia.Infrastructure.BackgroundJobs;

[DisallowConcurrentExecution]
internal sealed class ProcessOutboxMessagesJob : IJob
{
    private readonly TaskoPhobiaWriteDbContext _dbContext;
    private readonly DbSet<OutboxMessage> _outboxMessages;

    public ProcessOutboxMessagesJob(TaskoPhobiaWriteDbContext dbContext)
    {
        _outboxMessages = dbContext.OutboxMessages;
        _dbContext = dbContext;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        var messages = await _outboxMessages
            .Where(m => m.ProcessedDate == null)
            .OrderBy(x => x.OccurredOn)
            .Take(10)
            .ToListAsync();

        foreach (var outboxMessage in messages)
        {
            var domainNotification = JsonConvert
                .DeserializeObject<IDomainEventNotification>(outboxMessage.Data,
                    new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.All
                    });

            if (domainNotification is null) continue;

            //publish notification
            outboxMessage.ProcessedDate = DateTimeOffset.Now;
        }

        await _dbContext.SaveChangesAsync();
    }
}