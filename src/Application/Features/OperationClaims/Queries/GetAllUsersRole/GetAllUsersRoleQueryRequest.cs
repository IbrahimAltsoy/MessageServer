using Core.Application.Requests;
using Core.Application.Responses;
using MediatR;

namespace Application.Features.OperationClaims.Queries.GetAllUsersRole
{
    public class GetAllUsersRoleQueryRequest:IRequest<GetListResponse<GetAllUsersRoleQueryResponse>>
    {
        public PageRequest? PageRequest { get; set; } = null!;

    }
}
