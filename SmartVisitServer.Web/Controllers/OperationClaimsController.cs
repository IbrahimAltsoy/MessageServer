using Application.Features.OperationClaims.Command.Update;
using Microsoft.AspNetCore.Mvc;
using SmartVisitServer.Web.Services.OperationClaim;

namespace SmartVisitServer.Web.Controllers
{
    public class OperationClaimsController : Controller
    {
        readonly IOperationClaimService _operationClaimService;

        public OperationClaimsController(IOperationClaimService operationClaimService)
        {
            _operationClaimService = operationClaimService;
        }

        public async Task<IActionResult> Index(int page = 0, int pageSize = 5)
        {
            var result = await _operationClaimService.GetAllUsersRoleAsync(page, pageSize);
            return View(result);
        }
        public async Task<IActionResult> GetAllRols()
        {
            var result = await _operationClaimService.GetAllOperationClaimsAsync();
            return View(result);
        }
        public async Task<IActionResult> UpdateRol(UpdateOperationClaimCommandRequest request)
        {   
            var result = await _operationClaimService.UpdateRolAsync(request);
            return RedirectToAction("GetAllRols");
        }
    }
}
