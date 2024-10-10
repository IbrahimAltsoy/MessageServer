using Application.Features.Panel.Command.UpdateUserStatus;
using Application.Features.Panel.Queries.CreatedCompanyLastMontly;
using Application.Features.Panel.Queries.UserMemberShipLastDay;
using Core.Application.Requests;
using Core.Application.Responses;
using Domain.Enums;

namespace SmartVisitServer.Web.Services.Panels
{
    public interface IPanelService
    {
        Task<GetListResponse<UserMemberShipLastDayQueryResponse>> UserMemberShipLastDayGetAllAsync(int page, int pageSize);
        Task<UpdateUserStateResponse> UpdateUserStateAsync(Guid id, UserStatus userStatus);
        Task<GetListResponse<CreatedCompanyLastMontlyQueryResponse>> CreatedCompanyLastMontlyAsync(int page, int pageSize);
    }
}
