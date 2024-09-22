using Microsoft.AspNetCore.Mvc;

namespace SmartVisitServer.Web.ViewComponents.AdminLayout
{
    public class _Mainheader : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
