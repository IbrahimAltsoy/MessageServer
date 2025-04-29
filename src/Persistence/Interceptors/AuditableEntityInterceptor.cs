using Application.Abstract.Common;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using Core.Persistence.Repositories;
using System.Security.Claims;
using Domain.Entities;
using Application.Services.UserService;

namespace Persistence.Interceptors
{
    public class AuditableEntityInterceptor : SaveChangesInterceptor
    {
        readonly IUser _currentUser;
        readonly TimeProvider _dateTime;

        public AuditableEntityInterceptor(IUser currentUser, TimeProvider dateTime)
        {
            _currentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));
            _dateTime = dateTime ?? throw new ArgumentNullException(nameof(dateTime));
        }

        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            if (eventData.Context == null)
                throw new ArgumentNullException(nameof(eventData.Context));

            UpdateEntities(eventData.Context).GetAwaiter().GetResult();

            return base.SavingChanges(eventData, result);
        }

        public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            if (eventData.Context == null)
                throw new ArgumentNullException(nameof(eventData.Context));

            await UpdateEntities(eventData.Context);

            return await base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        private async Task UpdateEntities(DbContext context)
        {
            var entries = context.ChangeTracker.Entries<Entity<Guid>>();
            
            var userClaims = _currentUser.Claims;
            var userId = _currentUser.Id ?? "Unknown";
            
            var authName = _currentUser.Name ?? "Unknown";
            
            var utcNow = _dateTime.GetUtcNow().UtcDateTime;

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Detached || entry.State == EntityState.Unchanged)
                    continue;

                if (entry.State == EntityState.Added)
                {
                    entry.Property(nameof(Entity<Guid>.CreatedDate)).CurrentValue = utcNow;
                    entry.Property(nameof(Entity<Guid>.CreatedBy)).CurrentValue = userId + "-" + authName;
                }

                if (entry.State == EntityState.Modified || entry.HasChangedOwnedEntities())
                {
                    if (!IsSoftDeleting(entry))
                    {
                        entry.Property(nameof(Entity<Guid>.UpdatedDate)).CurrentValue = utcNow;
                        entry.Property(nameof(Entity<Guid>.LastModifiedBy)).CurrentValue = userId + "-" + authName;
                    }

                    entry.Property(nameof(Entity<Guid>.CreatedDate)).IsModified = false;
                    entry.Property(nameof(Entity<Guid>.CreatedBy)).IsModified = false;
                }

                if (entry.State == EntityState.Deleted || IsSoftDeleting(entry))
                {
                    if (ShouldHardDelete(entry))
                    {
                        continue; // CustomerPhoto gerçekten silinecek, dokunma
                    }

                    entry.Property(nameof(Entity<Guid>.DeletedDate)).CurrentValue = utcNow;
                    entry.Property(nameof(Entity<Guid>.DeletedBy)).CurrentValue = userId + "-" + authName;
                    entry.State = EntityState.Modified;
                }
            }

        }
        private static bool ShouldHardDelete(EntityEntry entry)
        {
            return entry.Entity.GetType() == typeof(CustomerPhoto);
        }


        private bool IsSoftDeleting(EntityEntry entry)
        {
            return entry.State == EntityState.Modified &&
                   entry.Property(nameof(Entity<Guid>.DeletedDate)).CurrentValue != null &&
                   entry.Property(nameof(Entity<Guid>.DeletedDate)).OriginalValue == null;
        }
    }

    public static class EntityExtensions
    {
        public static bool HasChangedOwnedEntities(this EntityEntry entry) =>
            entry.References.Any(r =>
                r.TargetEntry != null &&
                r.TargetEntry.Metadata.IsOwned() &&
                (r.TargetEntry.State == EntityState.Added || r.TargetEntry.State == EntityState.Modified));
    }


}
