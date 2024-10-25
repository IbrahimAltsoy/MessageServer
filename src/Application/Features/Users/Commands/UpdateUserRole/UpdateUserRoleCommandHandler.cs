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
        readonly IUserOperationClaimRepository _userOperationClaimRepository;
        readonly IUserRepository _userRepository;
        

        public UpdateUserRoleCommandHandler(IMapper mapper, IUserOperationClaimRepository userOperationClaimRepository, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userOperationClaimRepository= userOperationClaimRepository;
            _userRepository = userRepository;
           
        }

        public async Task<UpdateUserRoleCommandResponse> Handle(UpdateUserRoleCommandRequest request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetAsync(u => u.Id == request.UserId, include: x => x.Include(x => x.UserOperationClaims));
            var userOperationClaims = await _userOperationClaimRepository.GetListAsync(x => x.UserId == user.Id);
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
                        await _userOperationClaimRepository.AddAsync(newClaim);
                        user.UserOperationClaims.Add(newClaim);
                        anyClaimUpdated = true;
                    }
                }
                else
                {                   
                    if (userOperationClaim != null)
                    {
                        user.UserOperationClaims.Remove(userOperationClaim);
                        await _userOperationClaimRepository.DeleteAsync(userOperationClaim, true);
                        anyClaimUpdated = true;
                    }
                }
            }
            if (anyClaimUpdated) await _userRepository.UpdateAsync(user);
            var mappedData = _mapper.Map<UpdateUserRoleCommandResponse>(user);
            return mappedData;
        }
    }
}
