using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;

namespace Application.Features.OperationClaims.Queries.GetAll
{
    public class OperationClaimGetAllQueryHandler : IRequestHandler<OperationClaimGetAllQueryRequest, GetListResponse<OperationClaimGetAllQueryResponse>>
    {
        readonly IMapper _mapper;
        readonly IOperationClaimRepository _repository;

        public OperationClaimGetAllQueryHandler(IMapper mapper, IOperationClaimRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<GetListResponse<OperationClaimGetAllQueryResponse>> Handle(OperationClaimGetAllQueryRequest request, CancellationToken cancellationToken)
        {
            IPaginate<OperationClaim> datas = await _repository.GetPaginateListAsync(
                size: request.PageRequest.PageSize,
                index: request.PageRequest.Page,                
                cancellationToken: cancellationToken);
            IList<OperationClaimGetAllQueryResponse> data = _mapper.Map<IList<OperationClaimGetAllQueryResponse>>(datas.Items).ToList();
            GetListResponse<OperationClaimGetAllQueryResponse> responses = new GetListResponse<OperationClaimGetAllQueryResponse>
            {
                Index = datas.Index,
                Size = datas.Size,
                Count = datas.Count,
                Pages = datas.Pages,
                HasPrevious = datas.HasPrevious,
                HasNext = datas.HasNext,
                Items = data,
            };
            return responses;
        }
    }
}
