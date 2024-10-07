using Application.Features.Panel.Queries.UserMemberShipLastDay;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;
using SmartVisitServer.Web.Models;
using SmartVisitServer.Web.Services.Panels;
using System.Diagnostics;

namespace SmartVisitServer.Web.Controllers
{

    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        readonly IPanelService _panelService;

        public HomeController(IHttpClientFactory httpClientFactory, IPanelService panelService)
        {
            _httpClientFactory = httpClientFactory;
            _panelService = panelService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await _panelService.UserMemberShipLastDayGetAllAsync(0, 2);
            return View(result);

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [HttpGet]
        public async Task<IActionResult> UserMemberShipLastDays(int page, int pageSize)
        {

            return ViewComponent("UserMemberShipLastDays", new { page = page, pageSize = pageSize });
        }
     
        public async Task<PartialViewResult> UserSon(int page=0, int pageSize=2)
        {
            GetListResponse<UserMemberShipLastDayQueryResponse> result = await _panelService.UserMemberShipLastDayGetAllAsync(page, pageSize);
            return PartialView(result);
        }
    }

}
