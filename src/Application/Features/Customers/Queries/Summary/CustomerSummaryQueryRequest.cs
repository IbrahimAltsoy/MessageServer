using Domain.Enums;
using MediatR;

namespace Application.Features.Customers.Queries.Summary
{
    public class CustomerSummaryQueryRequest:IRequest<CustomerSummaryQueryResponse>
    {
        public TimePeriodType? TimePeriod { get; set; } = TimePeriodType.Daily;
    }
}
