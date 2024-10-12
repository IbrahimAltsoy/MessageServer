using Microsoft.AspNetCore.Mvc;
using SmartVisitServer.Web.Services.OperationClaim;

namespace SmartVisitServer.Web.Controllers
{
    public class SettingsController : Controller
    {
        readonly IOperationClaimService _operationClaimService;

        public SettingsController(IOperationClaimService operationClaimService)
        {
            _operationClaimService = operationClaimService;
        }

        public async Task<IActionResult> Index(int page=0, int pageSize=10)
        {
            var result = await _operationClaimService.GetAllUsersRoleAsync(page, pageSize);
            return View(result);
        }
    }
}
