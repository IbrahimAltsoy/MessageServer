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
            var user = await _userRepository.GetAsync(u => u.Id == request.UserId, include: x => x.Include(x => x.UserOperationClaims));
            var userOperationClaims = await _repository.GetListAsync(x => x.UserId == user.Id);
            bool anyClaimUpdated = false;

            foreach (var claimUpdate in request.OperationClaimUpdates)
            {
                var userOperationClaim = userOperationClaims
                    .FirstOrDefault(uoc => uoc.OperationClaimId == claimUpdate.OperationClaimId);

                if (claimUpdate.IsAssigned)
                {
                    if (userOperationClaim == null)
                    {
                        var newClaim = new UserOperationClaim(userId: request.UserId, operationClaimId: claimUpdate.OperationClaimId);
                        
                        await _repository.AddAsync(newClaim);
                        user.UserOperationClaims.Add(newClaim);
                        anyClaimUpdated = true;
                    }
                }
                else
                {
                   
                    if (userOperationClaim != null)
                    {
                        user.UserOperationClaims.Remove(userOperationClaim);
                        await _repository.DeleteAsync(userOperationClaim, true);
                        anyClaimUpdated = true;
                    }
                }
            }
            if (anyClaimUpdated)
            {
                await _userRepository.UpdateAsync(user);
            }
            return new UpdateUserRoleCommandResponse();
        }


    }
}
