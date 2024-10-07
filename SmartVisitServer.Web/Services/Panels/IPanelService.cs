using Application.Features.Panel.Queries.UserMemberShipLastDay;
using Core.Application.Requests;
using Core.Application.Responses;

namespace SmartVisitServer.Web.Services.Panels
{
    public interface IPanelService
    {
        Task<GetListResponse<UserMemberShipLastDayQueryResponse>> UserMemberShipLastDayGetAllAsync(int page, int pageSize);
    }
}
