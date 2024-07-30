using Core.Application.Requests;
using Domain.Enums;
using MediatR;

namespace Application.Features.Reports.Queries.ReportCustomer
{
    public class ReportCustomerQueryRequest:IRequest<ReportCustomerQueryResponse>
    {
        public TimePeriodType? TimePeriod { get; set; } = TimePeriodType.Daily;
       
    }
}
