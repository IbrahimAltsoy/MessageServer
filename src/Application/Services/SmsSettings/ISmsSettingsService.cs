using Domain.Entities;
using Domain.Enums;

namespace Application.Services.SmsSettings
{
    public interface ISmsSettingsService
    {
        Task<bool> SMSSettingsControlAsync(Guid? userId, SmsEventType eventType);
        Task<Customer?> GetByPhoneNumberAsync(Guid? userId,string phone);
    }
}
