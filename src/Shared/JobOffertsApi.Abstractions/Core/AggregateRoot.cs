using JobOffersApi.Abstractions.Kernel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JobOffersApi.Abstractions.Core;

public abstract class AggregateRoot<TId> : Entity<TId> where TId : struct
{
    private readonly List<IDomainEvent> mDomainEvents = new();

    public IReadOnlyCollection<IDomainEvent> GetDomainEvents() => mDomainEvents.ToList();

    public void ClearDomainEvents() => mDomainEvents.Clear();

    protected void RaiseDomainEvent(IDomainEvent domainEvent) =>
        mDomainEvents.Add(domainEvent);
}