using Microsoft.AspNetCore.Mvc;

namespace SmartVisitServer.Web.Controllers
{
    public class BaseController : Controller
    {
        public const string apiUrl = "http://localhost:5011/api";
        
        public IActionResult Index()
        {
            return View();
        }
    }
}
