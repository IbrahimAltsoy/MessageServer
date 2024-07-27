using Domain.Enums;

namespace Application.Helpers
{
    public static class SmsEventTypeExtensions
    {
        private static readonly Dictionary<SmsEventType, string> _eventContentDictionary = new Dictionary<SmsEventType, string>
        {
            { SmsEventType.ProductReceived, "Ürününüz teslim alınmıştır." },
            { SmsEventType.ProductIsReady, "Ürününüz teslim edilmek için hazırdır." }
        };

        public static string GetContent(this SmsEventType eventType)
        {
            return _eventContentDictionary.TryGetValue(eventType, out var content) ? content : string.Empty;
        }
    }
}
