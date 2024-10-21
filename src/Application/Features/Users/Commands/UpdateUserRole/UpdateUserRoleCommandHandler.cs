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

            // Adım 3: OperationClaimUpdates listesi üzerinden döngü kur
            foreach (var claimUpdate in request.OperationClaimUpdates)
            {
                // Kullanıcının sahip olduğu OperationClaim'lerden ilgili olanı bul
                var userOperationClaim = userOperationClaims
                    .FirstOrDefault(uoc => uoc.OperationClaimId == claimUpdate.OldOperationClaimId);

                // Eğer ilgili OperationClaim bulunduysa, güncelle
                if (userOperationClaim != null)
                {
                    // Önce, yeni atamak istediğimiz OperationClaim'in kullanıcıda zaten var olup olmadığını kontrol et
                    bool isAlreadyAssigned = userOperationClaims
                        .Any(uoc => uoc.OperationClaimId == claimUpdate.NewOperationClaimId);

                    // Eğer kullanıcıya bu rol zaten atanmışsa, bu güncellemeyi atla
                    if (isAlreadyAssigned)
                    {
                        continue; // Aynı rol atanmışsa, güncellemeyi yapmadan geç
                    }

                    // Güncelleme yap ve OperationClaimId'yi yeni ID ile değiştir
                    userOperationClaim.OperationClaimId = claimUpdate.NewOperationClaimId;
                    anyClaimUpdated = true;

                    // Güncellemeyi veritabanına kaydet
                    await _repository.UpdateAsync(userOperationClaim);
                }
            }
            return null;
        }

    }
}
