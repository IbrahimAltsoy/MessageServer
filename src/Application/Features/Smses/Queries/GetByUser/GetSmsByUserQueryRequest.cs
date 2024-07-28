using Application.Features.Users.Queries.GetById;
using Core.Application.Requests;
using Domain.Dtos.Smses;
using Domain.Enums;
using MediatR;

namespace Application.Features.Smses.Queries.GetByUser
{
    public class GetSmsByUserQueryRequest:IRequest<IList<SmsGetDto>>
    {
        public TimePeriodType? TimePeriod { get; set; } = TimePeriodType.Daily;
        public PageRequest PageRequest { get; set; }
    }
}
