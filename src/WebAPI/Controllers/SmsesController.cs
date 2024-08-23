using Application.Features.Smses.Commands.CreatedSmsDelivery;
using Application.Features.Smses.Queries.GetAll;
using Application.Features.Smses.Queries.GetByCustomer;
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
        [HttpGet("GetAllSmsesByUserId")]
        public async Task<IActionResult> GetAllSmsesByUserId([FromQuery]GetSmsByUserQueryRequest request)
        {
            IList<SmsGetDto> response = await Mediator.Send(request);
            return Ok(response);
        }
        [HttpGet("GetAllSmsesByCustomerId")]
        public async Task<IActionResult> GetAllSmsesByCustomerId([FromQuery] GetSmsByCustomerQueryRequest request)
        {
            IList<SmsGetDto> response = await Mediator.Send(request);
            return Ok(response);
        }
        [HttpGet("getAllSmsesByTimePeriod")]
        public async Task<IActionResult> GetAllSmsesByTimePeriod()
        {
            return Ok();
        }
        [HttpGet("GetSms")]// burası sadece uygulama sahibine açılacak yani admin panelde olacak bu mobilde olmayacak
        public async Task<IActionResult> GetSms([FromQuery]SmsGetAllQueryRequest request)
        {
            IList<SmsGetDto> response = await Mediator.Send(request);
            return Ok(response);
        }
        [HttpPost("CreatedSmsDelivery")]
        public async Task<IActionResult> CreatedSmsDelivery(CreatedSmsDeliveryCommandRequest request)
        {
            CreatedSmsDeliveryCommandResponse response = await Mediator.Send(request);
            return Ok(response);
        }
        [HttpPost("CreatedSmsPrivateDays")]
        public async Task<IActionResult> CreatedSmsPrivateDays()
        {
            return Ok();
        }
        [HttpPost("CreatedSmsHolidayDays")]
        public async Task<IActionResult> CreatedSmsHolidayDays()
        {
            return Ok();
        }

    }
}
