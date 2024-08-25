using Application.Features.Reports.Queries.ReportCustomer;
using Application.Features.Reports.Queries.ReportsCustomerDetail;
using Application.Features.Reports.Queries.ReportSms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]// silinen müşterileri de ekle ayrı olsun 
    [ApiController]
    public class ReportsController : BaseController
    {
        [HttpGet("SmsReports")]
        public async Task<IActionResult> SmsReports([FromQuery]ReportSmsQueryRequest request)
        {
            ReportSmsQueryResponse response = await Mediator.Send(request);
            return Ok(response);
        }
        [HttpGet("CustomerReports")]
        public async Task<IActionResult> CustomerReports([FromQuery]ReportCustomerQueryRequest request)
        {
            ReportCustomerQueryResponse response = await Mediator.Send(request);

            return Ok(response);
        }
        [HttpPost("ReportCustomerDetail")]
        public async Task<IActionResult> ReportCustomerDetail([FromQuery]ReportCustomerDetailQueryRequest request)
        {
            ReportCustomerDetailQueryResponse resposne = await Mediator.Send(request);
            return Ok(resposne);
        }
    }
}
