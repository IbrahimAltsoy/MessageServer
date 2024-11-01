using Application.Features.Reports.Queries.ReportCustomer;
using Application.Features.Reports.Queries.ReportsCustomerDetail;
using Application.Features.Reports.Queries.ReportSms;
using Domain.Enums;

namespace SmartVisitServer.Web.Services.Reports
{
    public interface IReportService
    {
        Task<ReportSmsQueryResponse> SmsReportsAsync(TimePeriodType? periodType);
        Task<ReportCustomerQueryResponse> CustomerReportsAsync(TimePeriodType? periodType);
    }
}
