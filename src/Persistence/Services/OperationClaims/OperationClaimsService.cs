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
    }
}
