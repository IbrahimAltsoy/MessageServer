using Core.Application.Requests;
using Core.Application.Responses;
using MediatR;

namespace Application.Features.Panel.Queries.UserMemberShipLastDay
{
    public class UserMemberShipLastDayQueryRequest:IRequest<GetListResponse<UserMemberShipLastDayQueryResponse>>
    {
        public PageRequest PageRequest { get; set; }       
    }
}
