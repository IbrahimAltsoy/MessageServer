using Application.Abstract.Common;
using Application.Services.Repositories;
using Application.Services.SmsService;
using Application.Services.SmsSettings;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using Persistence.Repositories;

namespace Persistence.SmsSettings
{
    public class SmsSettingsService : ISmsSettingsService
    {
        readonly BaseDbContext _context;
       
        readonly ISmsDefaultTemplateRepository _smsDefaulttemplateRepository;
        readonly ISmsCustomTemplateRepository _customTemplateRepository;
        readonly IUser _currentUser;
        readonly ISmsService _smsService;
        readonly ISmsRepository _smsRepository;
       
        public SmsSettingsService(BaseDbContext context, ISmsDefaultTemplateRepository smsDefaulttemplateRepository, ISmsCustomTemplateRepository customTemplateRepository, IUser currentUser, ISmsService smsService, ISmsRepository smsRepository)
        {
            _context = context;           
            _smsDefaulttemplateRepository = smsDefaulttemplateRepository;
            _customTemplateRepository = customTemplateRepository;            
            _currentUser = currentUser;
            _smsService = smsService;
            _smsRepository = smsRepository;
        }

        public async Task<bool> SMSSettingsControlAsync(Guid? userId, SmsEventType eventType)
        {
            if (!Guid.TryParse(_currentUser.Id, out Guid id))
            {
                throw new ArgumentException("Current user ID is not a valid GUID.");
            }
            userId = id;
           
            var smsSettings = await _context.SmsSettingies.FirstOrDefaultAsync(s => s.UserId == userId);
            if (smsSettings == null) return false;
            bool sendSms = false;
            switch (eventType)
            {
                case SmsEventType.ProductReceived:
                    sendSms = smsSettings.ProductReceived;
                    break;
                case SmsEventType.ProductIsReady:
                    sendSms = smsSettings.ProductIsReady;
                    break;
            }
            if (!sendSms) return false;           
            var template = await _customTemplateRepository.GetTemplateByUserIdAndNameAsync(userId, eventType.ToString()) ?? await _smsDefaulttemplateRepository.GetTemplateByNameAsync(eventType);
            return sendSms;
        }
    }
}
