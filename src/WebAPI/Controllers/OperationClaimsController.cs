using Application.Features.OperationClaims.Command.Create;
using Application.Features.OperationClaims.Command.Delete;
using Application.Features.OperationClaims.Command.Update;
using Application.Features.OperationClaims.Queries.GetAll;
using Application.Features.OperationClaims.Queries.GetAllUsersRole;
using Application.Features.OperationClaims.Queries.GetById;
using Core.Application.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Persistence.Contexts;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationClaimsController : BaseController
    {
        readonly BaseDbContext _context;
        public OperationClaimsController(BaseDbContext context)
        {
            _context = context;
        }
        [HttpGet("GetById")]
        public async Task<IActionResult> GetById([FromQuery] OperationClaimGetByIdQueryRequest request)
        {
            OperationClaimGetByIdQueryResponse response = await Mediator.Send(request);
            return Ok(response);
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll([FromQuery] OperationClaimGetAllQueryRequest request)
        {            
            IList<OperationClaimGetAllQueryResponse> response = await Mediator.Send(request);
            return Ok(response);
        }
        [HttpGet("GetAllUserRole")]
        public async Task<IActionResult> GetAllUserRole([FromQuery] GetAllUsersRoleQueryRequest request)
        {
            GetListResponse<GetAllUsersRoleQueryResponse> response = await Mediator.Send(request);
            return Ok(response);
        }
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CreateOperationClaimsCommandRequest request)
        {
            CreateOperationClaimsCommandResponse response = await Mediator.Send(request);
            return Ok(response);
        }
        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] UpdateOperationClaimCommandRequest request)
        {
            UpdateOperationClaimCommandResponse response = await Mediator.Send(request);
            return Ok(response);
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([FromQuery] DeleteOperationClaimCommandRequest request)
        {
            DeleteOperationClaimCommandResponse response = await Mediator.Send(request);
            return Ok(response);
        }
        //[HttpDelete("Delete")]
        //public async Task<IActionResult> Delete([FromQuery] Guid id)
        //{
        //   var data = await _context.OperationClaims.FindAsync(id);
        //    _context.OperationClaims.Remove(data!);
        //    _context.SaveChanges();
        //    return Ok(data);
        //}
    }
}
