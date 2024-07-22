using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class PasswordResetTokenRepository : EfRepositoryBase<PasswordResetToken, Guid, BaseDbContext>, IPasswordResetTokenRepository
{
    public PasswordResetTokenRepository(BaseDbContext context) : base(context)
    {
    }
}
