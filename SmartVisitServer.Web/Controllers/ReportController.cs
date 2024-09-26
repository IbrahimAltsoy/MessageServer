using Application.Abstract.Common;
using Microsoft.AspNetCore.Mvc;

namespace SmartVisitServer.Web.Controllers
{
    public class ReportController : BaseController
    {
        readonly IUser _currentUser;
        public ReportController(IUser currentUser)
        {
            _currentUser = currentUser;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var ap = $"{apiUrl}/reports";
            var name = _currentUser.Name ;
            ViewData["name"] = name ;
            return View();
        }
    }
}
