using Microsoft.AspNetCore.Mvc;

namespace SmartVisitServer.Web.Controllers
{
    public class ReportController : BaseController
    {
        [HttpGet]
        public IActionResult Index()
        {
            var ap = $"{apiUrl}/reports";
            return View();
        }
    }
}
