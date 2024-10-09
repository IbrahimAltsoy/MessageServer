using Application.Features.Panel.Command.UpdateUserStatus;
using Core.Application.Requests;
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

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]      
        public async Task<IActionResult> UserMemberShipLastDays(int page, int pageSize)
        {
            // Sayfa ve boyut bilgilerini kullanarak veriyi al
            var result = await _panelService.UserMemberShipLastDayGetAllAsync(page, pageSize);

            return ViewComponent("UserMemberShipLastDays", new { model = result });
        }
        [HttpPost]
        public async Task<IActionResult> UpdateUserStatus(Guid id, UserStatus userStatus)
        {
            if (id == Guid.Empty)
            {
                TempData["ErrorMessage"] = "Geçersiz kullanıcı ID'si.";
                return RedirectToAction("UserList");
            }

            // Servis katmanında kullanıcı durumunu güncelleme işlemi
            var result = await _panelService.UpdateUserStateAsync(id, userStatus);

            //if (result.Success)
            //{
            //    TempData["SuccessMessage"] = "Kullanıcı durumu başarıyla güncellendi.";
            //}
            //else
            //{
            //    TempData["ErrorMessage"] = "Kullanıcı durumu güncellenemedi: " + result.Message;
            //}

            return RedirectToAction("UserList"); // Durum güncellendikten sonra kullanıcı listesine dön
        }



    }
}
