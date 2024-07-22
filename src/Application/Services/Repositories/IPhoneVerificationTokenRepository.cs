using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Services.Repositories;

public interface IPhoneVerificationTokenRepository : IAsyncRepository<PhoneVerificationToken, Guid>, IRepository<PhoneVerificationToken, Guid> { }