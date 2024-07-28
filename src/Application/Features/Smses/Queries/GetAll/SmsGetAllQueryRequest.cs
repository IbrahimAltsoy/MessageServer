using Core.Application.Requests;
using Domain.Dtos.Smses;
using Domain.Enums;
using MediatR;

namespace Application.Features.Smses.Queries.GetAll
{
    public class SmsGetAllQueryRequest:IRequest<IList<SmsGetDto>>
    {
        public TimePeriodType? TimePeriod { get; set; } = TimePeriodType.Daily;
        public PageRequest PageRequest { get; set; }
    }
}
