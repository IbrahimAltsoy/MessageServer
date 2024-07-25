using Core.Persistence.Repositories;
using Domain.Dtos.SmsTemplate;
using Domain.Entities;
using Domain.Enums;

namespace Application.Services.Repositories
{
    public interface ISmsCustomTemplateRepository : IAsyncRepository<SmsCustomTemplate, Guid>, IRepository<SmsCustomTemplate, Guid>
    {
        Task<SmsTemplateDto> GetTemplateByUserIdAndNameAsync(Guid? userId, string? eventType);
    }
}
