using Core.Application.Requests;
using Domain.Enums;
using MediatR;

namespace Application.Features.Reports.Queries.ReportsCustomerDetail
{
    public class ReportCustomerDetailQueryRequest:IRequest<ReportCustomerDetailQueryResponse>
    {
        public TimePeriodType? TimePeriod { get; set; }
        public PageRequest PageRequest { get; set; }
        public bool SaveReport { get; set; }
    }
}
