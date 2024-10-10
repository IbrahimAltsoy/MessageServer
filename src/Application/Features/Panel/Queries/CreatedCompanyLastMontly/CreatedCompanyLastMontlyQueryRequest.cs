using Core.Application.Requests;
using Core.Application.Responses;
using MediatR;

namespace Application.Features.Panel.Queries.CreatedCompanyLastMontly
{
    public class CreatedCompanyLastMontlyQueryRequest:IRequest<GetListResponse<CreatedCompanyLastMontlyQueryResponse>>
    {
        public PageRequest PageRequest { get; set; }
    }
}
