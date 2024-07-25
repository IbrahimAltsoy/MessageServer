using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Services.Repositories
{
    public interface ISmsTemplateRepository : IAsyncRepository<SmsTemplate, Guid>, IRepository<SmsTemplate, Guid> { }
}
