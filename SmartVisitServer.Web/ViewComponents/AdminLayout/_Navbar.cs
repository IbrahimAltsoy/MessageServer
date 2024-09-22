using Microsoft.AspNetCore.Mvc;

namespace SmartVisitServer.Web.ViewComponents.AdminLayout
{
    public class _Navbar : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
