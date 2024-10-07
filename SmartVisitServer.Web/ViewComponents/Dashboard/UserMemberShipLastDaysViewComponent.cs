using Application.Features.Panel.Queries.UserMemberShipLastDay;
using Core.Application.Requests;
using Microsoft.AspNetCore.Mvc;
using SmartVisitServer.Web.Services.Panels;

namespace SmartVisitServer.Web.ViewComponents.Dashboard
{
    [ViewComponent]
    public class UserMemberShipLastDaysViewComponent:ViewComponent
    {
        readonly IPanelService _panelService;

        public UserMemberShipLastDaysViewComponent(IPanelService panelService)
        {
            _panelService = panelService;
        }
        [HttpGet]
        public async Task<IViewComponentResult> InvokeAsync(int page, int pagesize)
        {
            var results = await _panelService.UserMemberShipLastDayGetAllAsync(page, pagesize);

            return View("/Views/Dashboard/UserMemberShipLastDays/Default.cshtml",results);
        }
    }
}
