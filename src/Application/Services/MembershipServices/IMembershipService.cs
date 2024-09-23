using Domain.Entities;

namespace Application.Services.MembershipServices
{
    public interface IMembershipService
    {
        public Task<Membership> CreateMembershipAsync(Guid userId);

        // mevcut üyeliği getirme
        public Task<Membership> GetMembershipByIdAsync(Guid userId);

        //satın alma ile 
        public Task<Membership> MembershipPurchaseAsync(Guid userId, Guid membershipPackageId);

        //tanımlama ile -- sms
        public Task<Membership> DefineMembershipSmsAsync(Guid userId, int smsCount);

        //tanımlama ile -- gün
        public Task<Membership> DefineMembershipDayAsync(Guid userId, DateTime day);

        //tanımlama ile -- şirket sayısı
        public Task<Membership> DefineMembershipCompanyCountAsync(Guid userId, int companyCount);

        // ekleme ile -- sms
        public Task<Membership> AddMembershipSmsAsync(Guid userId, int addSms);

        //ekleme ile -- gün
        public Task<Membership> AddMembershipDayAsync(Guid userId, int addDay);

        //ekleme ile -- şirket sayısı
        public Task<Membership> AddMembershipCompanyCountAsync(Guid userId, int addCompanyCount);

        //üyelik sonlandırma
        public Task<Membership> MembershipTerminationAsync(Guid userId);

        //üyelik kontrolü - sms
        public Task<bool> MembershipSmsControl(Guid userId);

        //üyelik kontrolü - gün
        public Task<bool> MembershipDayControl(Guid userId);

        //üyelik kontrolü - şirket sayısı
        public Task<bool> MembershipCompanyCountControl(Guid userId);
    }
}
