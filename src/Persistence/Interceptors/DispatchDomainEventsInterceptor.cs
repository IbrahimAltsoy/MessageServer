﻿using MediatR;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Persistence.Repositories;

namespace Persistence.Interceptors
{
    //public class DispatchDomainEventsInterceptor : SaveChangesInterceptor
    //{
    //    readonly IMediator _mediator;

    //    public DispatchDomainEventsInterceptor(IMediator mediator)
    //    {
    //        _mediator = mediator;
    //    }
    //    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    //    {
    //        DispatchDomainEvents(eventData.Context).GetAwaiter().GetResult();
    //        return base.SavingChanges(eventData, result);
    //    }

    //    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    //    {
    //        await DispatchDomainEvents(eventData.Context);
    //        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    //    }

    //    public async Task DispatchDomainEvents(DbContext? context)
    //    {
    //        if (context == null) return;

    //        var entities = context.ChangeTracker
    //            .Entries<Entity<Guid>>()
    //            .Where(e => e.Entity.DomainEvents.Any())
    //            .Select(e => e.Entity);

    //        var domainEvents = entities
    //            .SelectMany(e => e.DomainEvents)
    //            .ToList();

    //        entities.ToList().ForEach(e => e.ClearDomainEvents());

    //        foreach (var domainEvent in domainEvents)
    //            await _mediator.Publish(domainEvent);
    //    }
    //}
}
