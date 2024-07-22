using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Services.Repositories;

public interface IContactFormRepository : IAsyncRepository<ContactForm, Guid>, IRepository<ContactForm, Guid>
{
}
