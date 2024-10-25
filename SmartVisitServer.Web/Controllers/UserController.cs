using Application.Features.Users.Commands.UpdateUserRole;
using Microsoft.AspNetCore.Mvc;
using SmartVisitServer.Web.Services.Users;
using ZXing;

namespace SmartVisitServer.Web.Controllers
{
    public class UserController : BaseController
    {
        readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UpdateUserRole([FromBody] UpdateUserRoleCommandRequest request)
        {
            var result = await _userService.UpdateUserRoleAsync(request);
            return Json(result);
        }
    }
}
