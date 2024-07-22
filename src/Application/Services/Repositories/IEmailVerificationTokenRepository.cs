using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Services.Repositories;

public interface IEmailVerificationTokenRepository : IAsyncRepository<EmailVerificationToken, Guid>, IRepository<EmailVerificationToken, Guid> { }