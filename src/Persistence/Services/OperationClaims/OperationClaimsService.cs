using Application.Services.OperationClaimService;
using Application.Services.Repositories;
using Core.Security.Constants;
using Domain.Entities;

namespace Persistence.Services.OperationClaims
{
    public class OperationClaimsService : IOperationClaimServices
    {
        readonly IOperationClaimRepository _operationClaimRepository;
        readonly IUserOperationClaimRepository _userOperationClaimRepository;

        public OperationClaimsService(IOperationClaimRepository operationClaimRepository, IUserOperationClaimRepository userOperationClaimRepository)
        {
            _operationClaimRepository = operationClaimRepository;
            _userOperationClaimRepository = userOperationClaimRepository;
        }

        public async Task RegisterUserSetUserClaimAsync(User user)
        {
            OperationClaim? getUserClaim = await _operationClaimRepository.GetAsync(c => c.Name == GeneralOperationClaims.User);
            if (getUserClaim is null)
                return;

            await _userOperationClaimRepository.AddAsync(new(user.Id, getUserClaim.Id));

        }
        public async Task<string> AddUserRoleIfNotExistsAsync(User user, string roleName)
        {
            var userClaims = await _userOperationClaimRepository.GetListAsync(u => u.UserId == user.Id);
            OperationClaim? operationClaim = await _operationClaimRepository.GetAsync(c => c.Name == roleName);

            if (operationClaim == null)
                return "Role bulunamadı.";

            bool hasRole = userClaims.Any(uoc => uoc.OperationClaimId == operationClaim.Id);
            if (hasRole)
                return "Kullanıcı bu role zaten sahiptir.";
                      
            await _userOperationClaimRepository.AddAsync(new(user.Id, operationClaim.Id));
            return "Role başarıyla eklendi.";
        }

    }
}
