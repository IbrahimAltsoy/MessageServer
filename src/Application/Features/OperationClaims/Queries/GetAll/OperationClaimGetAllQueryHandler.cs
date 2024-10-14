using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;

namespace Application.Features.OperationClaims.Queries.GetAll
{
    public class OperationClaimGetAllQueryHandler : IRequestHandler<OperationClaimGetAllQueryRequest, IList<OperationClaimGetAllQueryResponse>>
    {
        readonly IMapper _mapper;
        readonly IOperationClaimRepository _repository;

        public OperationClaimGetAllQueryHandler(IMapper mapper, IOperationClaimRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<IList<OperationClaimGetAllQueryResponse>> Handle(OperationClaimGetAllQueryRequest request, CancellationToken cancellationToken)
        {
            ICollection<OperationClaim> datas = await _repository.GetListAsync(                               
                cancellationToken: cancellationToken);
            IList<OperationClaimGetAllQueryResponse> responses = _mapper.Map<IList<OperationClaimGetAllQueryResponse>>(datas).ToList();
            
            return responses;
        }
    }
}
