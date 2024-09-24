using Application.Services.Repositories;
using AutoMapper;
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
            IPaginate<OperationClaim> datas = await _repository.GetPaginateListAsync(
                size: request.PageRequest.PageSize,
                index: request.PageRequest.Page,                
                cancellationToken: cancellationToken);
            IList<OperationClaimGetAllQueryResponse> responses = _mapper.Map<IList<OperationClaimGetAllQueryResponse>>(datas.Items).ToList();
            return responses;
        }
    }
}
