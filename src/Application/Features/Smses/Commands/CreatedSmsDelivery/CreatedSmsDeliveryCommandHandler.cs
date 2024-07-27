using Application.Abstract.Common;
using Application.Helpers;
using Application.Services.Repositories;
using Application.Services.SmsService;
using Application.Services.SmsSettings;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Features.Smses.Commands.CreatedSmsDelivery
{
    public class CreatedSmsDeliveryCommandHandler : IRequestHandler<CreatedSmsDeliveryCommandRequest, CreatedSmsDeliveryCommandResponse>
    {
        readonly IMapper _mapper;
        readonly ISmsRepository _smsRepository;
        readonly IUser _currentUser;
        readonly ISmsService _smsService;
        readonly ISmsSettingsService _smsSettingsService;
           

        public CreatedSmsDeliveryCommandHandler(
            IMapper mapper, 
            ISmsRepository smsRepository, 
            IUser currentUser,
            ISmsService smsService,
            ISmsSettingsService smsSettingsService)
        {
            _mapper = mapper;
            _smsRepository = smsRepository;
            _currentUser = currentUser;
            _smsService = smsService;
            _smsSettingsService = smsSettingsService;
        }

        public async Task<CreatedSmsDeliveryCommandResponse> Handle(CreatedSmsDeliveryCommandRequest request, CancellationToken cancellationToken)
        {
            if (!Guid.TryParse(_currentUser.Id, out Guid userId))
            {
                throw new ArgumentException("Current user ID is not a valid GUID.");
            }
            bool sendSms = await _smsSettingsService.SMSSettingsControlAsync(userId, SmsEventType.ProductIsReady);
            if (sendSms)
            {
                string eventContent = SmsEventType.ProductIsReady.GetContent();
                var message = $"Sayın {request.NameSurname} {request.ProductName} {eventContent} {_currentUser.CompnanyName}";
                await _smsService.SendSms(request.Phone,message );
                Sms sms = new Sms
                {
                    Content = message,
                    CustomerId = request.CustomerId,
                    UserId = userId,

                };
                await _smsRepository.AddAsync(sms);
                return new()
                {
                    Message = "Ürün teslimi için başarılı bir şekilde mesaj atılmıştır."
                };
            }
            return new()
            {
                Message = "Mesaj ayarlarınız kapalı olduğundan mesaj gönderilmemiştir."
            };

            
        }
    }
}
