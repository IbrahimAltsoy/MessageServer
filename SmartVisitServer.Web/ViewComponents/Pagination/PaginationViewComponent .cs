using Microsoft.AspNetCore.Mvc;
using SmartVisitServer.Web.Models.Paginate;

namespace SmartVisitServer.Web.ViewComponents.PaginationModel
{
    [ViewComponent]
    public class PaginationViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke<T>(PagedList<T> model, string actionName, string controllerName)
        {
            ViewData["ActionName"] = actionName;
            ViewData["ControllerName"] = controllerName;
            return View(model);
        }
    }

}
