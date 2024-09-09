using Microsoft.AspNetCore.Mvc;

namespace SmartVisitServer.Web.ViewComponents.AdminLayout
{
    public class _Scripttemplate : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
