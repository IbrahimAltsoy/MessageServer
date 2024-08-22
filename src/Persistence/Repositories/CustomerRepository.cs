using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Dtos.Customers;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

namespace Persistence.Repositories
{
    public class CustomerRepository : EfRepositoryBase<Customer, Guid, BaseDbContext>, ICustomerRepository
    {
        public CustomerRepository(BaseDbContext context) : base(context)
        {
        }

        
    }
}