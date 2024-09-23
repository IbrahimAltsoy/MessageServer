namespace Persistence.Services.AppSettingServices
{
    public static class AppSettingParameters
    {
        public const string MaintenanceMod = "MaintenanceMod";

        // server saati
        public const string ServerUTC = "ServerUTC";

        // yeni uye kaydi izni
        public const string RegisterNewUser = "RegisterNewUser";

        // yeni uye icin mail dogrulamasi
        public const string RegisterNewUserEmailVerification = "RegisterNewUserEmailVerification";

        // uygulama magazasi
        public const string ProductStore = "ProductStore";

        // abone bulteni kaydi
        public const string Subscribers = "Subscribers";

        // sms gonderme izni 
        public const string SendSms = "SendSms";

        // mail gonderme izni
        public const string SendMail = "SendMail";

        // bilgilendirme gonderme izni
        public const string SendNotification = "SendNotification";

        // yeni firma olusturulunca varsayilan sms sayisi
        public const string NewCompanySmsCount = "NewCompanySmsCount";

        // yeni firma olusturulunca varsayilan gun sayisi
        public const string NewCompanyDayCount = "NewCompanyDayCount";

        // yeni uye kaydi olusturulunca varsayilan sirket sayisi
        public const string NewCompanyCompanyCount = "NewCompanyCompanyCount";

        // email dogrulanmasi yapmayan kisiler sisteme giris yapabilir mi izni
        public const string LoginWithNotEmailVerification = "LoginWithNotEmailVerification";

        // sms şablonu: alınan sipariş
        public const string SmsReceivingOrderKey = "SmsReceivingOrder";
        public const string SmsReceivingOrderValue = "Sayın {customerName}, ürünleriniz {day} gün içerisinde alınacaktır.";

        // sms şablonu: siparişi teslim almak
        public const string SmsReceiveOrderDeliveryKey = "SmsReceiveOrderDelivery";
        public const string SmsReceiveOrderDeliveryValue = "Sayın {customerName}, {quantity} ürününüz teslim alınmıştır. Fiyat {price} TL dir.";

        // sms şablonu: siparişi teslim listesine alma
        public const string SmsDeliveryListKey = "SmsDeliveryList";
        public const string SmsDeliveryListValue = "Sayın {customerName}, ürünleriniz gün içerisinde teslim edilecektir. Sipariş tutarınız {price} TL dir.";

        // sms şablonu: siparişi teslim etme
        public const string SmsDeliveryKey = "SmsDelivery";
        public const string SmsDeliveryValue = "Sayın {customerName}, ürünleriniz teslim edilmiştir. Firmamızı tercih ettiğiniz için teşekkürler.";

        // sms şablonu: siparişi iptal etme
        public const string SmsOrderCancellationKey = "SmsOrderCancellation";
        public const string SmsOrderCancellationValue = "Sayın {customerName}, siparişiniz iptal edilmiştir.";

        // sms şablonu: sipariş teslim edilemedi-evde yok
        public const string SmsNotAtHomeKey = "SmsNotAtHome";
        public const string SmsNotAtHomeValue = "Sayın {customerName}, {time} itibariyle adresinize geldik fakat size ulaşamadık. Lütfen firmamızı arayarak teslim randevusu oluşturun.";

        // sms şablonu: sipariş tutarı
        public const string SmsOrderAmountKey = "SmsOrderAmount";
        public const string SmsOrderAmountValue = "Sayın {customerName}, ürünlerinizin fiyatı {price} TL'dir.";
    }
}
