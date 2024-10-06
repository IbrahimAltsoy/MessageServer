using Core.Application.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SmartVisitServer.Web.Services.Panels;
using System.ComponentModel;

namespace SmartVisitServer.Web.Controllers
{
    public class PanelController : Controller
    { 
        readonly IPanelService _panelService;

        public PanelController(IPanelService panelService)
        {
            _panelService = panelService;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]      
        public async Task<IActionResult> UserMemberShipLastDays(PageRequest pageRequest)
        {
            // Sayfa ve boyut bilgilerini kullanarak veriyi al
            var result = await _panelService.UserMemberShipLastDayGetAllAsync(pageRequest.Page, pageRequest.PageSize);

            return ViewComponent("UserMemberShipLastDays", new { model = result });
        }


    }
}
