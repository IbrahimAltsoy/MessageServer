using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Services.Repositories
{
    public interface ICustomerPhotoRepository : IAsyncRepository<CustomerPhoto, Guid>, IRepository<CustomerPhoto, Guid>
    {
    }
}

