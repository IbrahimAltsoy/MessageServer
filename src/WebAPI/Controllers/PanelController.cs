using Application.Features.Panel.Command.UpdateUserStatus;
using Application.Features.Panel.Queries.ActiveOrPasifUsers;
using Application.Features.Panel.Queries.UserMemberShipLastDay;
using Application.Features.Panel.Queries.UserStateUsersIstatic;
using Core.Application.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PanelController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> UserTotalPasifAndActive([FromQuery]UserTotalPasifAndActiveQueryRequest request)
        {
            UserTotalPasifAndActiveQueryResponse response = await Mediator.Send(request);
            return Ok(response);
        }
        [HttpGet("UserIstatics")]
        public async Task<IActionResult> UserIstatics([FromQuery]UserStateUsersIstaticQueryRequest request)
        {
            UserStateUsersIstaticQueryResponse response = await Mediator.Send(request);
            return Ok(response);
        }
        [HttpGet("UserMemberShipLastDays")]
        public async Task<IActionResult> UserMemberShipLastDays([FromQuery] UserMemberShipLastDayQueryRequest request)
        {
           GetListResponse< UserMemberShipLastDayQueryResponse> response = await Mediator.Send(request);
            return Ok(response);
        }
        [HttpPost("UpdateUserStatu")]
        public async Task<IActionResult> UpdateUserStatu([FromBody] UpdateUserStateRequest request)
        {
            UpdateUserStateResponse response = await Mediator.Send(request);
            return Ok(response);
        }
    }
}
