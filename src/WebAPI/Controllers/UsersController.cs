using Application.Features.Users.Commands.UpdateProfile;
using Application.Features.Users.Commands.UpdateUserRole;
using Application.Features.Users.Queries.GetById;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseController
    {
        [HttpGet("Profile")]
        public async Task<IActionResult> Profile([FromQuery] GetByIdUserQuery request )
        {
            GetByIdUserResponse resonse = await Mediator.Send(request);
            return Ok(resonse);
        }
        [HttpPost("UpdateUserRole")]
        public async Task<IActionResult> UpdateUserRole([FromBody] UpdateUserRoleCommandRequest request)
        {
            UpdateUserRoleCommandResponse response = await Mediator.Send(request);
            return Ok(response);
        }
        [HttpPost("UpdateUserProfile")]
        public async Task<IActionResult> UpdateUserRole([FromBody] UpdateProfileCommandRequest request)
        {
            UpdateProfileCommandResponse response = await Mediator.Send(request);
            return Ok(response);
        }

    }
}
