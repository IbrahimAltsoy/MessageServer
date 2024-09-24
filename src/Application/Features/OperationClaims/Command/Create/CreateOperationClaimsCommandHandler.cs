using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.OperationClaims.Command.Create
{
    public class CreateOperationClaimsCommandHandler : IRequestHandler<CreateOperationClaimsCommandRequest, CreateOperationClaimsCommandResponse>
    {
        readonly IMapper _mapper;
        readonly IOperationClaimRepository _repository;

        public CreateOperationClaimsCommandHandler(IMapper mapper, IOperationClaimRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<CreateOperationClaimsCommandResponse> Handle(CreateOperationClaimsCommandRequest request, CancellationToken cancellationToken)
        {
            var data = _mapper.Map<OperationClaim>(request);
            var createdData =await _repository.AddAsync(data);
            var response = _mapper.Map<CreateOperationClaimsCommandResponse>(createdData);
            return response;
        }
    }
}
