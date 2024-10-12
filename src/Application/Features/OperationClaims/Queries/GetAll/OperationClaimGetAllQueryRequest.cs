using Core.Application.Pipelines.Authorization;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Security.Constants;
using MediatR;

namespace Application.Features.OperationClaims.Queries.GetAll
{
    public class OperationClaimGetAllQueryRequest:IRequest<GetListResponse<OperationClaimGetAllQueryResponse>>/*,ISecuredRequest*/
    {
        //public string[] Roles => new[] { GeneralOperationClaims.Admin };
        public PageRequest? PageRequest { get; set; }=null!;
        
    }
}
