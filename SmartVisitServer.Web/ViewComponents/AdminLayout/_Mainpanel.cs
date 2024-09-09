using Microsoft.AspNetCore.Mvc;

namespace SmartVisitServer.Web.ViewComponents.AdminLayout
{
    public class _Mainpanel : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
