using Microsoft.AspNetCore.Mvc;

namespace SmartVisitServer.Web.ViewComponents.Dashboard
{
    [ViewComponent]
    public class UserListChartViewComponent : ViewComponent
    {

        public async Task<IViewComponentResult> InvokeAsync()
        {
           
            return View("/Views/Dashboard/UserListChart/Default.cshtml");
        }
    }
}
