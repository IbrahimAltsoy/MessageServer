using Application.Abstract.Common;
using Domain.Enums;
using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Mvc;
using SmartVisitServer.Web.Services.Reports;

namespace SmartVisitServer.Web.Controllers
{
    public class ReportController : BaseController
    {
        readonly IUser _currentUser;
        readonly IReportService _reportService;
        public ReportController(IUser currentUser, IReportService reportService)
        {
            _currentUser = currentUser;
            _reportService = reportService;
        }
        [HttpGet]
        public IActionResult Index()
        {            
            return View();
        }
        [HttpGet]
        public async Task<PartialViewResult> SmsReports(TimePeriodType? periodType=TimePeriodType.Yearly)
        {
            var result = await _reportService.SmsReportsAsync(periodType);
            return PartialView(result);
        }
        [HttpGet]
        public async Task<PartialViewResult> CustomerReports(TimePeriodType? periodType=TimePeriodType.Yearly)
        {
            var result = await _reportService.CustomerReportsAsync(periodType);
            return PartialView(result);
        }
    }
}
