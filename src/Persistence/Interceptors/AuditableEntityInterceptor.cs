using Application.Abstract.Common;
using Application.Services.UserService;
using Core.Persistence.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Npgsql;
using System;
using System.Collections.Concurrent;
using System.Data;

namespace Persistence.Interceptors;

public class AuditableEntityInterceptor : SaveChangesInterceptor
{
    private const string UnknownUser = "SYSTEM";
   

    private static readonly ConcurrentDictionary<Type, bool> HardDeleteEntities = new()
    {
        [typeof(CustomerPhoto)] = true
    };

    
    private readonly TimeProvider _timeProvider;
    private readonly IUser _currentUser;

    public AuditableEntityInterceptor(       
        TimeProvider timeProvider, IUser currentUser)
    {       
        _timeProvider = timeProvider;
        _currentUser = currentUser;
    }

    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        if (eventData.Context is null)
            return await base.SavingChangesAsync(eventData, result, cancellationToken);

        await using (var transaction = await eventData.Context.Database.BeginTransactionAsync(
            IsolationLevel.ReadCommitted, cancellationToken))
        {
            try
            {
                await ApplyAuditRulesAsync(eventData.Context, cancellationToken);
                await transaction.CommitAsync(cancellationToken);
            }
            catch
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
        }

        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private async Task ApplyAuditRulesAsync(
        DbContext context,
        CancellationToken cancellationToken)
    {
        var utcNow = _timeProvider.GetUtcNow().UtcDateTime;
        var user = _currentUser.Name;
        var userId = _currentUser.Id ?? UnknownUser;
        var userEmail = _currentUser.Email ?? UnknownUser;

        foreach (var entry in context.ChangeTracker.Entries<Entity<Guid>>())
        {
            if (entry.State is EntityState.Detached or EntityState.Unchanged)
                continue;

            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Property(x => x.CreatedDate).CurrentValue = utcNow;
                    entry.Property(x => x.CreatedBy).CurrentValue = $"{userId}|{userEmail}";
                    break;

                case EntityState.Modified when !IsSoftDeleting(entry):
                    entry.Property(x => x.UpdatedDate).CurrentValue = utcNow;
                    entry.Property(x => x.LastModifiedBy).CurrentValue = $"{userId}|{userEmail}";
                    entry.Property(x => x.CreatedDate).IsModified = false;
                    entry.Property(x => x.CreatedBy).IsModified = false;
                    break;

                case EntityState.Deleted when !ShouldHardDelete(entry):
                    entry.Property(x => x.DeletedDate).CurrentValue = utcNow;
                    entry.Property(x => x.DeletedBy).CurrentValue = $"{userId}|{userEmail}";
                    entry.State = EntityState.Modified;
                    break;
            }
        }
    }

    private static bool ShouldHardDelete(EntityEntry entry) =>
        HardDeleteEntities.ContainsKey(entry.Entity.GetType());

    private static bool IsSoftDeleting(EntityEntry entry) =>
        entry.State == EntityState.Modified &&
        entry.Property(nameof(Entity<Guid>.DeletedDate)).CurrentValue != null &&
        entry.Property(nameof(Entity<Guid>.DeletedDate)).OriginalValue == null;
}

public static class EntityExtensions
{
    public static bool HasChangedOwnedEntities(this EntityEntry entry) =>
        entry.References.Any(r =>
            r.TargetEntry != null &&
            r.TargetEntry.Metadata.IsOwned() &&
            (r.TargetEntry.State is EntityState.Added or EntityState.Modified));
}