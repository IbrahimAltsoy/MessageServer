using Application.Features.Customers.Queries.CustomerGetAllByUser;
using Application.Features.OperationClaims.Queries.GetAll;
using Application.Features.OperationClaims.Queries.GetAllUsersRole;
using Core.Application.Responses;
using Domain.Enums;

namespace SmartVisitServer.Web.Services.OperationClaim
{
    public interface IOperationClaimService
    {
        Task<GetListResponse<OperationClaimGetAllQueryResponse>> GetAllOperationClaimsAsync(int page, int pageSize);
        Task<GetListResponse<GetAllUsersRoleQueryResponse>> GetAllUsersRoleAsync(int page, int pageSize);

    }
}
