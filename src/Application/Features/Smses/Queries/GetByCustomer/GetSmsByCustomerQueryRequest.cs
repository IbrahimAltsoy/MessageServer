using Core.Application.Requests;
using Domain.Dtos.Smses;
using Domain.Enums;
using MediatR;

namespace Application.Features.Smses.Queries.GetByCustomer
{
    public class GetSmsByCustomerQueryRequest:IRequest<IList<SmsGetDto>>
    {
        public Guid CustomerId { get; set; }
        public TimePeriodType? TimePeriod { get; set; } = TimePeriodType.Daily;
        public PageRequest PageRequest { get; set; }
    }
}
