using Application.Services.AppSettingService;
using Application.Services.MembershipServices;
using Application.Services.Repositories;
using Domain.Entities;
using Persistence.Services.AppSettingServices;

namespace Persistence.Services.MembershipServices
{
    public class MembershipService : IMembershipService
    {
        private readonly IAppSettingService _appSettingService;
        private readonly IMembershipRepository _membershipRepository;
        private readonly IMembershipPackageRepository _membershipPackageRepository;
        private readonly IUserRepository _userRepository;
        private readonly MembershipBusinessRules _membershipBusinessRules;

        public MembershipService(IAppSettingService appSettingService,IMembershipRepository membershipRepository, IMembershipPackageRepository membershipPackageRepository, IUserRepository userRepository, MembershipBusinessRules membershipBusinessRules)
        {
            _appSettingService = appSettingService;
            _membershipRepository = membershipRepository;
            _membershipPackageRepository = membershipPackageRepository;
            _userRepository = userRepository;
            _membershipBusinessRules = membershipBusinessRules;
        }
        public async Task<Membership> CreateMembershipAsync(Guid userId)
        {
            int smsCount = int.Parse(await _appSettingService.GetParameterAsync(AppSettingParameters.NewCompanySmsCount));
            int day = int.Parse(await _appSettingService.GetParameterAsync(AppSettingParameters.NewCompanyDayCount));
            int companyCount = int.Parse(await _appSettingService.GetParameterAsync(AppSettingParameters.NewCompanyCompanyCount));

            Membership newMembership = new Membership { UserId = userId, LastDay = DateTime.UtcNow.AddDays(day), SmsCount = smsCount, CompanyCount = companyCount };
            Membership createdMembership = await _membershipRepository.AddAsync(newMembership);
            return createdMembership;
        }

        public async Task<Membership> MembershipPurchaseAsync(Guid userId, Guid membershipPackageId)
        {
            Membership getMembership = await GetMembershipByIdAsync(userId);
            MembershipPackage? getPackage = await _membershipPackageRepository.GetAsync(c => c.Id == membershipPackageId);

            getMembership.SmsCount += getPackage!.SmsCount;
            getMembership.CompanyCount = getPackage!.CompanyCount;
            getMembership.LastDay = MembershipAddDay(getMembership.LastDay ?? DateTime.Today, getPackage.AddDay ?? 0);

            Membership updatedMembership = await _membershipRepository.UpdateAsync(getMembership);
            return updatedMembership;
        }

        public async Task<Membership> DefineMembershipCompanyCountAsync(Guid userId, int companyCount)
        {
            Membership getMembership = await GetMembershipByIdAsync(userId);
            getMembership.CompanyCount = companyCount;

            var defineMembership = await _membershipRepository.UpdateAsync(getMembership);
            return defineMembership;
        }

        public async Task<Membership> DefineMembershipDayAsync(Guid userId, DateTime day)
        {
            Membership getMembership = await GetMembershipByIdAsync(userId);
            getMembership.LastDay = day;

            var defineMembership = await _membershipRepository.UpdateAsync(getMembership);
            return defineMembership;
        }

        public async Task<Membership> DefineMembershipSmsAsync(Guid userId, int smsCount)
        {
            Membership getMembership = await GetMembershipByIdAsync(userId);
            getMembership.SmsCount = smsCount;

            var defineMembership = await _membershipRepository.UpdateAsync(getMembership);
            return defineMembership;
        }

        public async Task<bool> MembershipCompanyCountControl(Guid userId)
        {
            Membership getMembership = await GetMembershipByIdAsync(userId);
            int companyCount = _userRepository.Count(c => c.Id == userId);
            if (getMembership.CompanyCount > companyCount)
                return true;

            return false;
        }

        public async Task<bool> MembershipDayControl(Guid userId)
        {
            Membership getMembership = await GetMembershipByIdAsync(userId);
            if (getMembership.LastDay <= DateTime.UtcNow)
                return true;

            return false;
        }
        public async Task<bool> MembershipSmsControl(Guid userId)
        {
            Membership getMembership = await GetMembershipByIdAsync(userId);
            if (getMembership.SmsCount > 0)
                return true;

            return false;
        }

        public async Task<Membership> GetMembershipByIdAsync(Guid userId)
        {
            Membership? getMembership = await _membershipRepository.GetAsync(c => c.UserId == userId, enableTracking: false);
            await _membershipBusinessRules.MembershipNullControl(getMembership!);
            return getMembership!;
        }

        public async Task<Membership> MembershipTerminationAsync(Guid userId)
        {
            Membership getMembership = await GetMembershipByIdAsync(userId);
            getMembership.SmsCount = 0;
            getMembership.LastDay = DateTime.UtcNow;
            getMembership.CompanyCount = 0;

            Membership updatedMembership = await _membershipRepository.UpdateAsync(getMembership);
            return updatedMembership;
        }

        public async Task<Membership> AddMembershipSmsAsync(Guid userId, int addSms)
        {
            Membership getMembership = await GetMembershipByIdAsync(userId);
            getMembership.SmsCount += addSms;

            var defineMembership = await _membershipRepository.UpdateAsync(getMembership);
            return defineMembership;
        }

        public async Task<Membership> AddMembershipDayAsync(Guid userId, int addDay)
        {
            Membership getMembership = await GetMembershipByIdAsync(userId);
            getMembership.LastDay = MembershipAddDay(Convert.ToDateTime(getMembership.LastDay), addDay);

            var defineMembership = await _membershipRepository.UpdateAsync(getMembership);
            return defineMembership;
        }

        public async Task<Membership> AddMembershipCompanyCountAsync(Guid userId, int addCompanyCount)
        {
            Membership getMembership = await GetMembershipByIdAsync(userId);
            getMembership.CompanyCount += addCompanyCount;

            var defineMembership = await _membershipRepository.UpdateAsync(getMembership);
            return defineMembership;
        }

        private DateTime MembershipAddDay(DateTime lastDay, int addDay)
        {
            if (lastDay < DateTime.UtcNow)
                lastDay = DateTime.UtcNow;

            DateTime newLastDay = lastDay.AddDays(addDay);
            return newLastDay;
        }

    }
}
