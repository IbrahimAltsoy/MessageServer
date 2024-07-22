using Domain.AdapterModels.SmsModels;
using Microsoft.AspNetCore.Http;
using System.Linq.Expressions;
using System.Xml.Serialization;

namespace Infrastructure.Adapter.MasSms;

public class MasSmsHelper
{
    public static Task<SmsResponseModel> ResultPretier(string result)
    {
        SmsResponseModel resultSmsModel = new();

        if (string.IsNullOrEmpty(result))
        {
            resultSmsModel.Status = "Bilinmeyen bir sorun oluştu";
            return Task.FromResult(resultSmsModel);
        }

        string[] splitResult = result.Split(" ");
        if (splitResult.Length > 0)
        {
            resultSmsModel.Status = _stringStatusCode(splitResult[0]);
        }
        if (splitResult.Length > 1)
        {
            resultSmsModel.Data = splitResult[1];
        }

        return Task.FromResult(resultSmsModel);
    }

    private static string _stringStatusCode(string statuCode)
    {
        switch (statuCode)
        {
            case "00":
                return "İşlem başarılı.";
            case "77":
                return "Hata. Son 2 dakika içinde aynı SMS gönderildi.";
            case "81":
                return "Gönderilecek olan mesaj için yeterli krediye sahip değilsiniz.";
            case "83":
                return "Mesaj metni ve numaralar incelendikten sonra sistem yollanacak bir SMS oluşturmaya yetecek en az 1 numara ve en az 1 karakterden oluşan mesaj metnine sahip olamadı.";
            case "84":
                return "İleri tarihli gönderim zamanı hatalı bir formata sahip veya 1 yıldan daha ileri bir zamanı gösteriyor";
            case "85":
                return "Belirttiğiniz mesaj başlığı bulunamadı veya onaylanmamış.";
            case "87":
                return "Kullanıcı adı veya şifre hatalı.";
            case "89":
                return "POST verisi XML olarak parse edilemedi.";
            case "91":
                return "POST verisi okunamadı veya yok.";
            case "97":
                return "İsteği HTTP POST ile yollayınız.";
            case "99":
                return "Henüz dokümante edilmemiş bir hata oldu.";
            default:
                return "Bilinmeyen bir sorun oluştu";
        }

    }
}
