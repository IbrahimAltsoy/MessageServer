using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Services.Repositories;

public interface IPasswordResetTokenRepository : IAsyncRepository<PasswordResetToken, Guid>, IRepository<PasswordResetToken, Guid> { }