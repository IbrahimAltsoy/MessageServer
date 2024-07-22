using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class EmailVerificationTokenRepository : EfRepositoryBase<EmailVerificationToken, Guid, BaseDbContext>, IEmailVerificationTokenRepository
{
    public EmailVerificationTokenRepository(BaseDbContext context) : base(context)
    {
    }
}