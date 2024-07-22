using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Services.Repositories;

public interface IUserCompanyDetailRepository : IRepository<UserCompanyDetail, Guid>, IAsyncRepository<UserCompanyDetail, Guid>
{
}
