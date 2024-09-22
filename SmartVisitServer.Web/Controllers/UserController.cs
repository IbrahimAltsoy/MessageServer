using Microsoft.AspNetCore.Mvc;

namespace SmartVisitServer.Web.Controllers
{
    public class UserController : BaseController
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
