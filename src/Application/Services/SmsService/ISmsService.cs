using Domain.AdapterModels.SmsModels;
using Domain.Enums;

namespace Application.Services.SmsService;

public interface ISmsService
{
    Task<SmsResponseModel> CreditInquiry(); // Kredi sorgulama
    Task<SmsResponseModel> TitleInquiry(); // Başlık sorgulama
    Task<object> SendSms(string phone, string message, DateTime sendDate = default(DateTime)); // Sms gönderme, tarih test edilecek.

}
