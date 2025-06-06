﻿using Application.Features.OperationClaims.Command.Update;
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
       
       
        public async Task<IActionResult> GetRoles()
        {
            var roles = await _operationClaimService.GetAllOperationClaimsAsync(); // Roller hizmetini kullanarak al
            var roleList = roles.Select(r => new { r.Name, r.Id });
            return Json(roleList); 
        }
        public async Task<IActionResult> GetRole(Guid id)
        {
            var role = await _operationClaimService.GetByIdUserRoleAsync(id);
            return Json(role); 
        }
        public async Task<IActionResult> UpdateRol(UpdateOperationClaimCommandRequest request)
        {
            var result = await _operationClaimService.UpdateRolAsync(request);
            return RedirectToAction("GetAllRols");
        }
        [HttpPost]
        public async Task<IActionResult> AddRole([FromForm]string name)
        {
            var result = await _operationClaimService.AddRoleAsync(name);
            return RedirectToAction("GetAllRols", "OperationClaims");
        }

    }
}
