using Application.Services.Repositories;

namespace Persistence.Services.MembershipServices
{
    public static class MembershipBusinessRulesMessages
    {
        public const string YouCannotOpenMoreCompany = "Daha fazla şirket açamazsınız";
        public const string MembershipIsNull = "Üyelik yok";
        public const string SmsLimitExpired = "Sms limitiniz doldu, lütfen paketinizi yenileyin";
        public const string LastDayExpired = "Üyeliğiniz bitti, lütfen yeni üyelik paketi satın alın";
    }
}
