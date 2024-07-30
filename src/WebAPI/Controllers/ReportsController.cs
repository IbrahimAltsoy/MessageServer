using Application.Features.Reports.Queries.ReportCustomer;
using Application.Features.Reports.Queries.ReportsCustomerDetail;
using Application.Features.Reports.Queries.ReportSms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : BaseController
    {
        [HttpGet("smsReports")]
        public async Task<IActionResult> SmsReports([FromQuery]ReportSmsQueryRequest request)
        {
            ReportSmsQueryResponse response = await Mediator.Send(request);
            return Ok(response);
        }
        [HttpGet("customerReports")]
        public async Task<IActionResult> CustomerReports([FromQuery]ReportCustomerQueryRequest request)
        {
            ReportCustomerQueryResponse response = await Mediator.Send(request);

            return Ok(response);
        }
        [HttpPost("reportCustomerDetail")]
        public async Task<IActionResult> ReportCustomerDetail([FromQuery]ReportCustomerDetailQueryRequest request)
        {
            ReportCustomerDetailQueryResponse resposne = await Mediator.Send(request);
            return Ok(resposne);
        }
    }
}
