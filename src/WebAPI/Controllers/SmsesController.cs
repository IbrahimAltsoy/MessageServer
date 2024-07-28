using Application.Features.Smses.Commands.CreatedSmsDelivery;
using Application.Features.Smses.Queries.GetByUser;
using Domain.Dtos.Smses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SmsesController : BaseController
    {
        [HttpGet("getAllSmsesByUserId")]
        public async Task<IActionResult> GetAllSmsesByUserId([FromQuery]GetSmsByUserQueryRequest request)
        {
            IList<SmsGetDto> response = await Mediator.Send(request);
            return Ok(response);
        }
        [HttpGet("getAllSmsesByCustomerId")]
        public async Task<IActionResult> GetAllSmsesByCustomerId()
        {
            return Ok();
        }
        [HttpGet("getAllSmsesByTimePeriod")]
        public async Task<IActionResult> GetAllSmsesByTimePeriod()
        {
            return Ok();
        }
        [HttpGet("getSms")]
        public async Task<IActionResult> GetSms()
        {
            return Ok();
        }
        [HttpPost("createdSmsDelivery")]
        public async Task<IActionResult> CreatedSmsDelivery(CreatedSmsDeliveryCommandRequest request)
        {
            CreatedSmsDeliveryCommandResponse response = await Mediator.Send(request);
            return Ok(response);
        }
        [HttpPost("createdSmsPrivateDays")]
        public async Task<IActionResult> createdSmsPrivateDays()
        {
            return Ok();
        }
        [HttpPost("createdSmsHolidayDays")]
        public async Task<IActionResult> CreatedSmsHolidayDays()
        {
            return Ok();
        }

    }
}
