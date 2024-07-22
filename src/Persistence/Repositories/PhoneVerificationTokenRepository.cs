using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class PhoneVerificationTokenRepository : EfRepositoryBase<PhoneVerificationToken, Guid, BaseDbContext>, IPhoneVerificationTokenRepository
{
    public PhoneVerificationTokenRepository(BaseDbContext context) : base(context)
    {
    }
}
