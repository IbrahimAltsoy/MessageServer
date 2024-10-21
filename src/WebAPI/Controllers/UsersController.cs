using Application.Features.Users.Commands.UpdateUserRole;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseController
    {
        [HttpPost("UpdateUserRole")]
        public async Task<IActionResult> UpdateUserRole([FromBody] UpdateUserRoleCommandRequest request)
        {
            UpdateUserRoleCommandResponse response = await Mediator.Send(request);
            return Ok(response);
        }
    }
}
