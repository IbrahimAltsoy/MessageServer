using Application.Features.Panel.Command.UpdateUserStatus;
using Application.Features.Panel.Queries.CreatedCompanyLastMontly;
using Application.Features.Panel.Queries.UserMemberShipLastDay;
using Core.Application.Requests;
using Core.Application.Responses;
using Domain.Enums;
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

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await _panelService.UserMemberShipLastDayGetAllAsync(0, 5);
            return View(result);

        }
        [HttpGet]
        public async Task<PartialViewResult> UserUserMemberShipLastDaysSon(int page = 0, int pageSize = 5)
        {
           /* GetListResponse<UserMemberShipLastDayQueryResponse> */ 
            var result = await _panelService.UserMemberShipLastDayGetAllAsync(page, pageSize);
            return PartialView(result);
        }
        [HttpGet]
        public async Task<IActionResult> CreatedCompanyLastMontly(int page=0, int pageSize = 5)
        {
            /*GetListResponse< CreatedCompanyLastMontlyQueryResponse >*/ 
            var responses = await _panelService.CreatedCompanyLastMontlyAsync(page, pageSize);
            return View(responses);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateUserStatus(Guid id, UserStatus userStatus)
        {
            if (id == Guid.Empty)
            {
                TempData["ErrorMessage"] = "Geçersiz kullanıcı ID'si.";
                return RedirectToAction("Index", "Panel");
            }
            var result = await _panelService.UpdateUserStateAsync(id, userStatus);

            return RedirectToAction("Index", "Panel"); 
        }



    }
}
