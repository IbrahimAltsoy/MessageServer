using Microsoft.AspNetCore.Mvc;

namespace SmartVisitServer.Web.ViewComponents.AdminLayout
{
    public class _Customtemplate : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
