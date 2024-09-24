using Application.Services.Repositories;
using AutoMapper;
using MediatR;

namespace Application.Features.OperationClaims.Queries.GetById
{
    public class OperationClaimGetByIdQueryHandler : IRequestHandler<OperationClaimGetByIdQueryRequest, OperationClaimGetByIdQueryResponse>
    {
        readonly IMapper _mapper;
        readonly IOperationClaimRepository _repository;

        public OperationClaimGetByIdQueryHandler(IMapper mapper, IOperationClaimRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<OperationClaimGetByIdQueryResponse> Handle(OperationClaimGetByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var data = await _repository.GetAsync(x=>x.Id==request.Id, cancellationToken:cancellationToken);
            if (data == null) return new();
            var response = _mapper.Map< OperationClaimGetByIdQueryResponse >(data);
            return response;

        }
    }
}
