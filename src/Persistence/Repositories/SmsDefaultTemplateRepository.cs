using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Dtos.SmsTemplate;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

namespace Persistence.Repositories
{
    public class SmsDefaultTemplateRepository : EfRepositoryBase<SmsDefaultTemplate, Guid, BaseDbContext>, ISmsDefaultTemplateRepository
    {
        public SmsDefaultTemplateRepository(BaseDbContext context) : base(context)
        {
        }

        public async Task<SmsTemplateDto> GetTemplateByNameAsync(SmsEventType eventType)
        { 
            if (eventType == null) return null;
            int eventTypeValue = (int)eventType;
            var template =await Context.SmsDefaultTemplates.Where(t => (int)t.SmsEventType == eventTypeValue).Select(t=> new SmsTemplateDto
            {
                Title = t.SmsEventType.ToString(),
                Content = t.Content,
            }).SingleOrDefaultAsync();
            return template;
        }
    }
}
