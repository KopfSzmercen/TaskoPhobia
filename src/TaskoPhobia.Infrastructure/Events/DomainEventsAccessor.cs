﻿using TaskoPhobia.Infrastructure.DAL.Contexts;
using TaskoPhobia.Shared.Abstractions.Domain;

namespace TaskoPhobia.Infrastructure.Events;

internal sealed class DomainEventsAccessor : IDomainEventsAccessor
{
    private readonly TaskoPhobiaWriteDbContext _dbContext;

    public DomainEventsAccessor(TaskoPhobiaWriteDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IReadOnlyCollection<IDomainEvent> GetAllDomainEvents()
    {
        var domainEntities = _dbContext.ChangeTracker
            .Entries<Entity>()
            .Where(x => x.Entity.DomainEvents.Any())
            .ToList();

        return domainEntities
            .SelectMany(x => x.Entity.DomainEvents)
            .ToList();
    }

    public void ClearAllDomainEvents()
    {
        var domainEntities = _dbContext.ChangeTracker
            .Entries<Entity>()
            .Where(x => x.Entity.DomainEvents.Any()).ToList();

        domainEntities
            .ForEach(x => x.Entity.ClearDomainEvents());
    }
}