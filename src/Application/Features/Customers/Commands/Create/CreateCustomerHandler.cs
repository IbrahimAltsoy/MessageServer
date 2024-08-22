using Application.Abstract.Common;
using Application.Services.Repositories;
using Application.Services.SmsService;
using Application.Services.SmsSettings;
using AutoMapper;
using Domain.Entities;
using Domain.Events;
using MediatR;

namespace Application.Features.Customers.Commands.Create
{
    public class CreateCustomerHandler : IRequestHandler<CreateCustomerRequest, CreateCustomerResponse>
    {// Customer düzelt ad ve soyadı beraber al tek tek alma ve Name propertisini ProductName olarak al
        readonly ICustomerRepository _customerRepository;
        readonly IMapper _mapper;
        readonly IUser _currentUser;
        readonly ISmsSettingsService _settingsService;
        readonly ISmsService _smsService;
        readonly ISmsRepository _smsRepository;

        public CreateCustomerHandler(
            ICustomerRepository customerRepository,
            IMapper mapper,IUser currentUser, 
            ISmsSettingsService settingsService, 
            ISmsService smsService, 
            ISmsRepository smsRepository)
        {
            _customerRepository = customerRepository;
            _currentUser = currentUser;
            _mapper = mapper;
            _settingsService = settingsService;
            _smsService = smsService;
            _smsRepository = smsRepository;
        }

        public async Task<CreateCustomerResponse> Handle(CreateCustomerRequest request, CancellationToken cancellationToken)
        {

            if (!Guid.TryParse(_currentUser.Id, out Guid userId))
            {
                throw new ArgumentException("Current user ID is not a valid GUID.");
            }

            var existingCustomer = await _settingsService.GetByPhoneNumberAsync(userId, request.Phone);
            if (existingCustomer != null)
            {
                existingCustomer.NameSurname = request.NameSurname;
                existingCustomer.ProductName = request.ProductName;
                existingCustomer.Phone = request.Phone;
                existingCustomer.Description = request.Description;
                existingCustomer.UserId = userId;
                
                if (existingCustomer.Pointed==10)
                {
                    existingCustomer.Pointed = 0;
                    bool sm = await _settingsService.SMSSettingsControlAsync(userId, Domain.Enums.SmsEventType.ProductReceived);
                    if (sm)
                    {
                        string message = $"{request.ProductName} ürününüz indirimden yapaılacaktır. {_currentUser.CompnanyName}";
                        await _smsService.SendSms(request.Phone, message);
                        var sms = new Sms
                        {
                            Content = message,
                            UserId = userId,
                            CustomerId = existingCustomer.Id
                        };
                        await _smsRepository.AddAsync(sms);
                    }


                }
                existingCustomer.Pointed += 1;
                await _customerRepository.UpdateAsync(existingCustomer);
               
               bool sendSms = await _settingsService.SMSSettingsControlAsync(userId, Domain.Enums.SmsEventType.ProductReceived);
                if(sendSms)
                {
                    string message = $"{request.ProductName} ürünü teslim alınmıştır. {_currentUser.CompnanyName}";
                   // await _smsService.SendSms(request.Phone, message);
                    var sms = new Sms
                    {
                        Content = message,
                        UserId = userId,
                        CustomerId = existingCustomer.Id
                    };
                    await _smsRepository.AddAsync(sms);
                }
                

                return new CreateCustomerResponse
                {
                    Message = $"ürün kaydı başarıyla gerçekleşti: {existingCustomer.ProductName ?? ""}",
                };
            }
            else
            {
                // Yeni müşteri ekle
                var newCustomer = _mapper.Map<Customer>(request);
                newCustomer.UserId = userId;

                var result = await _customerRepository.AddAsync(newCustomer);
                newCustomer.AddDomainEvent(new CustomerCreatedEvent(newCustomer));

                // SMS Ayarlarını kontrol et ve SMS gönder
              bool sendSms =  await _settingsService.SMSSettingsControlAsync(userId, Domain.Enums.SmsEventType.ProductReceived);
                if(sendSms)
                {
                    string message = $"{request.ProductName} ürünü teslim alınmıştır. {_currentUser.CompnanyName}";
                    await _smsService.SendSms(request.Phone, message);

                    var sms = new Sms
                    {
                        Content = message,
                        UserId = userId,
                        CustomerId = newCustomer.Id
                    };

                    await _smsRepository.AddAsync(sms);
                }

                

                return new CreateCustomerResponse
                {
                    Message = $"Ürün bilgisi başarıyla oluşturuldu:  {result.ProductName ?? ""}",
                };
            }
        }
    }
    
}
