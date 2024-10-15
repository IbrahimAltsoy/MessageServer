using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

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
            ICollection<OperationClaim> datas = await _repository.GetListAsync(include: x=>x.Include(x=>x.UserOperationClaims),                               
                cancellationToken: cancellationToken);
            var source = datas.Select(x => new OperationClaimGetAllQueryResponse()
            {
                Id = x.Id,
                Name = x.Name,
                UserCount = x.UserOperationClaims.Count()
            }).ToList();
            IList<OperationClaimGetAllQueryResponse> responses = _mapper.Map<IList<OperationClaimGetAllQueryResponse>>(source).ToList();            
            return responses;
        }
    }
}
