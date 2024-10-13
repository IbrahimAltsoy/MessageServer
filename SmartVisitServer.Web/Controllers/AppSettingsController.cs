using Application.Features.AppSettings.Commands.Create;
using Application.Features.AppSettings.Commands.Update;
using Microsoft.AspNetCore.Mvc;
using SmartVisitServer.Web.Services.AppSettings;
using SmartVisitServer.Web.Services.OperationClaim;

namespace SmartVisitServer.Web.Controllers
{
    public class AppSettingsController : Controller
    {
      readonly IAppSettingsService _appSettingsService;

        public AppSettingsController(IAppSettingsService appSettingsService)
        {
            _appSettingsService = appSettingsService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _appSettingsService.GetAllAppSettingGetAllQueryAsync();
            return View(result);
        }
        public async Task<IActionResult> CreateSetting(CreateAppSettingCommandRequest request)
        {
            var result = await _appSettingsService.AppSettingCreateAsync(request);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> UpdateSetting(UpdateAppSettingsCommandRequest request)
        {
            var result = await _appSettingsService.AppSettingUpdateAsync(request);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> DeleteSetting(Guid id)
        {
            await _appSettingsService.AppSettingDeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}
