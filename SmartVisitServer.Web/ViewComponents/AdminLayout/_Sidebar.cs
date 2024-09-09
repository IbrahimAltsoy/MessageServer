using Microsoft.AspNetCore.Mvc;

namespace SmartVisitServer.Web.ViewComponents.AdminLayout
{
    public class _Sidebar : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
