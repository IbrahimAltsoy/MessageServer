using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories
{
    public class CustomerPhotoRepository:EfRepositoryBase<CustomerPhoto, Guid, BaseDbContext>, ICustomerPhotoRepository
    {
        public CustomerPhotoRepository(BaseDbContext context) : base(context)
    {
    }
}
}
