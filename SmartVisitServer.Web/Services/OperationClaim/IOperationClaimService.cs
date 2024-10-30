using Application.Features.Customers.Queries.CustomerGetAllByUser;
using Application.Features.OperationClaims.Command.Create;
using Application.Features.OperationClaims.Command.Update;
using Application.Features.OperationClaims.Queries.GetAll;
using Application.Features.OperationClaims.Queries.GetAllUsersRole;
using Application.Features.OperationClaims.Queries.GetById;
using Core.Application.Responses;
using Domain.Enums;

namespace SmartVisitServer.Web.Services.OperationClaim
{
    public interface IOperationClaimService
    {
        Task<IList<OperationClaimGetAllQueryResponse>> GetAllOperationClaimsAsync();
        Task<GetListResponse<GetAllUsersRoleQueryResponse>> GetAllUsersRoleAsync(int page, int pageSize);
        Task<UpdateOperationClaimCommandResponse> UpdateRolAsync(UpdateOperationClaimCommandRequest request);
        Task<OperationClaimGetByIdQueryResponse> GetByIdUserRoleAsync(Guid id);
        Task<object> AddRoleAsync(string name);

    }
}
