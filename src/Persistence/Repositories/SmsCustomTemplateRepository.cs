using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Dtos.SmsTemplate;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

namespace Persistence.Repositories
{
    public class SmsCustomTemplateRepository : EfRepositoryBase<SmsCustomTemplate, Guid, BaseDbContext>, ISmsCustomTemplateRepository
    {
        public SmsCustomTemplateRepository(BaseDbContext context) : base(context)
        {
        }

        public async Task<SmsTemplateDto> GetTemplateByUserIdAndNameAsync(Guid? userId, string? eventType)
        {
            if (userId == null || eventType==null)
            {
                return null;
            }
            var template =await Context.SmsCustomTemplates
                .AsNoTracking()
                .Where(t => t.UserId == userId && t.Title == eventType).Select(t=>new SmsTemplateDto
                {
                    Title = t.Title,
                    Content = t.Content
                }).FirstOrDefaultAsync();
            return template ?? null;
        }
    }
}
