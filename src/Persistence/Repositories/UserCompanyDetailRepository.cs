using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class UserCompanyDetailRepository : EfRepositoryBase<UserCompanyDetail, Guid, BaseDbContext>, IUserCompanyDetailRepository
{
    public UserCompanyDetailRepository(BaseDbContext context) : base(context)
    {
    }
}
