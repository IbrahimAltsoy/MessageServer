﻿using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class NotificationRepository : EfRepositoryBase<Notification, Guid, BaseDbContext>, INotificationRepository
{
    public NotificationRepository(BaseDbContext context) : base(context)
    {
    }
}
