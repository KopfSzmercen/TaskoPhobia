using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using TaskoPhobia.Infrastructure.DAL.Contexts;
using TaskoPhobia.Shared.Abstractions.Domain;
using TaskoPhobia.Shared.Abstractions.Outbox;
using TaskoPhobia.Shared.Abstractions.Time;
using TaskoPhobia.Shared.Events;
using TaskoPhobia.Shared.Processing;

namespace TaskoPhobia.Infrastructure.Events;

internal sealed class DomainEventsDispatcher : IDomainEventsDispatcher
{
    private readonly IClock _clock;
    private readonly TaskoPhobiaWriteDbContext _dbContext;
    private readonly IDomainEventsAccessor _domainEventsAccessor;
    private readonly IServiceProvider _serviceProvider;

    public DomainEventsDispatcher(TaskoPhobiaWriteDbContext dbContext, IClock clock, IServiceProvider serviceProvider,
        IDomainEventsAccessor domainEventsAccessor)
    {
        _dbContext = dbContext;
        _clock = clock;
        _serviceProvider = serviceProvider;
        _domainEventsAccessor = domainEventsAccessor;
    }

    public async Task DispatchEventsAsync()
    {
        var domainEvents = _domainEventsAccessor.GetAllDomainEvents();
        var domainEventNotifications = new List<IDomainEventNotification<IDomainEvent>>();

        using var scope = _serviceProvider.CreateScope();
        foreach (var domainEvent in domainEvents)
        {
            var handlerType = typeof(IDomainEventHandler<>).MakeGenericType(domainEvent.GetType());
            var handlers = scope.ServiceProvider.GetServices(handlerType);

            var tasks = handlers.Select(x => (Task)handlerType
                .GetMethod(nameof(IDomainEventHandler<IDomainEvent>.HandleAsync))
                ?.Invoke(x, new object[] { domainEvent })
            );
            await Task.WhenAll(tasks);
        }

        foreach (var domainEvent in domainEvents)
        {
            var domainEventType = typeof(IDomainEvent);
            var domainNotificationType = typeof(DomainNotificationBase<>).MakeGenericType(domainEventType);

            if (Activator.CreateInstance(domainNotificationType, domainEvent, domainEvent.Id) is
                IDomainEventNotification<IDomainEvent> domainNotificationInstance)
                domainEventNotifications.Add(domainNotificationInstance);
        }

        var outboxMessages = ConvertDomainEventNotificationsIntoOutboxMessages(domainEventNotifications);
        _dbContext.Set<OutboxMessage>().AddRange(outboxMessages);
    }

    private IEnumerable<OutboxMessage> ConvertDomainEventNotificationsIntoOutboxMessages(
        IEnumerable<IDomainEventNotification> domainEventNotifications)
    {
        return domainEventNotifications.Select(domainEventNotification => new OutboxMessage
        (
            Guid.NewGuid(),
            _clock.DateTimeOffsetNow(),
            domainEventNotification.GetType().Name,
            JsonConvert.SerializeObject(
                domainEventNotification,
                new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All
                })
        )).ToList();
    }
}