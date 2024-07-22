using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class ContactFormRepository : EfRepositoryBase<ContactForm, Guid, BaseDbContext>, IContactFormRepository
{
    public ContactFormRepository(BaseDbContext context) : base(context)
    {
    }
}
