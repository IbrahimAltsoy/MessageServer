using Application.Abstract.Common;
using Application.Services.Repositories;
using Application.Services.SmsService;
using Application.Services.SmsSettings;
using AutoMapper;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using Persistence.Repositories;

namespace Persistence.SmsSettings
{
    public class SmsSettingsService : ISmsSettingsService
    {
        readonly BaseDbContext _context;
        readonly IMapper _mapper;
        readonly ISmsDefaultTemplateRepository _smsDefaulttemplateRepository;
        readonly ISmsCustomTemplateRepository _customTemplateRepository;
        readonly IUser _currentUser;
        readonly ISmsService _smsService;
        

        public SmsSettingsService(BaseDbContext context, IMapper mapper, ISmsDefaultTemplateRepository smsDefaulttemplateRepository, ISmsCustomTemplateRepository customTemplateRepository, ISmsService smsService, IUser currentUser)
        {
            _context = context;
            _mapper = mapper;
            _smsDefaulttemplateRepository = smsDefaulttemplateRepository;
            _customTemplateRepository = customTemplateRepository;
            _smsService = smsService;
            _currentUser = currentUser;
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
            //int eventTypeValue = (int)eventType; // buraya göre ayarla 
            var template = await _customTemplateRepository.GetTemplateByUserIdAndNameAsync(userId, eventType.ToString()) ?? await _smsDefaulttemplateRepository.GetTemplateByNameAsync(eventType);
            await _smsService.SendSms("5375092791",$"{template.Content} templati yakaladık.");
            return sendSms;
        }
    }
}
