using Microsoft.AspNetCore.Mvc;
namespace SmartVisitServer.Web.Controllers
{
    public class SmsController : BaseController
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
