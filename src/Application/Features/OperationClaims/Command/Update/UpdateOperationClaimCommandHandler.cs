using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.OperationClaims.Command.Update
{
    public class UpdateOperationClaimCommandHandler : IRequestHandler<UpdateOperationClaimCommandRequest, UpdateOperationClaimCommandResponse>
    {
        readonly IMapper _mapper;
        readonly IOperationClaimRepository _repository;

        public UpdateOperationClaimCommandHandler(IMapper mapper, IOperationClaimRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<UpdateOperationClaimCommandResponse> Handle(UpdateOperationClaimCommandRequest request, CancellationToken cancellationToken)
        {
           var data = _mapper.Map<OperationClaim>(request);
            var updatedData = await _repository.UpdateAsync(data);
            var response = _mapper.Map<UpdateOperationClaimCommandResponse>(updatedData);
            return response;
        }
    }
}
