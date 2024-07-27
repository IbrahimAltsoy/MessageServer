using Core.Persistence.Repositories;
using Domain.Dtos.Customers;
using Domain.Entities;

namespace Application.Services.Repositories
{
    public interface ICustomerRepository : IAsyncRepository<Customer, Guid>, IRepository<Customer, Guid> 
    {
        Task<Customer?> GetByPhoneNumberAsync(string phone);
    }
}
