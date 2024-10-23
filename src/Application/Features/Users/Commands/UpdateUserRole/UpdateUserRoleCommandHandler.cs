using Application.Abstract.Common;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Users.Commands.UpdateUserRole
{
    public class UpdateUserRoleCommandHandler : IRequestHandler<UpdateUserRoleCommandRequest, UpdateUserRoleCommandResponse>
    {
        readonly IMapper _mapper;
        readonly IUserOperationClaimRepository _repository;
        readonly IUserRepository _userRepository;
        readonly IOperationClaimRepository _operationClaimRepository;

        public UpdateUserRoleCommandHandler(IMapper mapper, IUserOperationClaimRepository repository, IUserRepository userRepository, IOperationClaimRepository operationClaimRepository)
        {
            _mapper = mapper;
            _repository = repository;
            _userRepository = userRepository;
            _operationClaimRepository = operationClaimRepository;
        }

        public async Task<UpdateUserRoleCommandResponse> Handle(UpdateUserRoleCommandRequest request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetAsync(u => u.Id == request.UserId);
            var userOperationClaims = await _repository.GetListAsync(x => x.UserId == user.Id);

            bool anyClaimUpdated = false;

            foreach (var claimUpdate in request.OperationClaimUpdates)
            {
             
                var userOperationClaim = userOperationClaims
                    .FirstOrDefault(uoc => uoc.OperationClaimId == claimUpdate.OldOperationClaimId);

             
                if (userOperationClaim != null)
                {
                 
                    bool isAlreadyAssigned = userOperationClaims
                        .Any(uoc => uoc.OperationClaimId == claimUpdate.NewOperationClaimId);

                   
                    if (isAlreadyAssigned)
                    {
                        continue;
                    }

                  
                    userOperationClaim.OperationClaimId = claimUpdate.NewOperationClaimId;
                    anyClaimUpdated = true;

                  
                    await _repository.UpdateAsync(userOperationClaim);
                }
            }
            return null;
        }

    }
}
