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

        public async Task<IViewComponentResult> InvokeAsync(int page =0, int pagesize =2)
        {
            var results = await _panelService.UserMemberShipLastDayGetAllAsync(page, pagesize);

            return View("/Views/Dashboard/UserMemberShipLastDays/Default.cshtml",results);
        }
    }
}
