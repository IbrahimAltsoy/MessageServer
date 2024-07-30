using Core.Application.Requests;
using Domain.Enums;
using MediatR;

namespace Application.Features.Reports.Queries.ReportSms
{
    public class ReportSmsQueryRequest:IRequest<ReportSmsQueryResponse>
    {
       
        public TimePeriodType? TimePeriod { get; set; } = TimePeriodType.Daily;
        
    }
}
