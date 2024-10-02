using Microsoft.AspNetCore.Mvc;
using L= Core.Application.Responses;

namespace SmartVisitServer.Web.ViewComponents.PaginationModel
{

    public class PaginationViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(object model, string actionName, string controllerName)
        {
            ViewData["ActionName"] = actionName;
            ViewData["ControllerName"] = controllerName;
            return View(model);
        }
    }

}
