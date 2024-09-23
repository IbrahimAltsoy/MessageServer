using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Persistence.Services.MembershipServices
{
    public class MembershipBusinessRules:BaseBusinessRules
    {
        readonly IMembershipRepository _membershipRepository;
        readonly IUserRepository _userRepository;

        public MembershipBusinessRules(IMembershipRepository membershipRepository, IUserRepository userRepository)
        {
            _membershipRepository = membershipRepository;
            _userRepository = userRepository;
        }
        public Task MembershipNullControl(Membership membership)
        {
            if (membership is null)
                throw new BusinessException(MembershipBusinessRulesMessages.MembershipIsNull);

            return Task.CompletedTask;
        }

        public async Task MembershipLastDayControl(Guid userId)
        {
            Membership membership = await GetMembership(userId);
            if (membership.LastDay <= DateTime.UtcNow)
                throw new BusinessException(MembershipBusinessRulesMessages.LastDayExpired);
        }

        public async Task MembershipSmsControl(Guid userId)
        {
            Membership membership = await GetMembership(userId);
            if (membership.SmsCount <= 0)
                throw new BusinessException(MembershipBusinessRulesMessages.SmsLimitExpired);
        }

        public async Task MembershipCompanyLimitControl(Guid userId)
        {
            Membership membership = await GetMembership(userId);
            int companyCount = _userRepository.Count(c => c.Id == userId);

            if (membership.CompanyCount <= companyCount)
                throw new BusinessException(MembershipBusinessRulesMessages.YouCannotOpenMoreCompany);
        }

        private async Task<Membership> GetMembership(Guid userId)
        {
            Membership? membership = await _membershipRepository.GetAsync(c => c.UserId == userId, enableTracking: false);
            if (membership == null)
                throw new BusinessException(MembershipBusinessRulesMessages.MembershipIsNull);
            return membership;
        }
    }
}
