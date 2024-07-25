using Core.Persistence.Repositories;
using Domain.Dtos.SmsTemplate;
using Domain.Entities;
using Domain.Enums;

namespace Application.Services.Repositories
{
    public interface ISmsDefaultTemplateRepository : IAsyncRepository<SmsDefaultTemplate, Guid>, IRepository<SmsDefaultTemplate, Guid> 
    {
       Task<SmsTemplateDto> GetTemplateByNameAsync(SmsEventType eventType);

    }
}
