using Application.Features.AppSettings.Commands.Create;
using Application.Features.AppSettings.Commands.Delete;
using Application.Features.AppSettings.Commands.Update;
using Application.Features.AppSettings.Queries.GetAll;
using Application.Features.AppSettings.Queries.GetById;
using Core.Application.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppSettingsController : BaseController
    {
        [HttpGet("Id")]
        public async Task<IActionResult> GetById([FromQuery] AppSettingGetByIdQueryRequest request)
        {
            AppSettingGetByIdQueryResponse response = await Mediator.Send(request);
            return Ok(response);
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> AppSettingGetAll([FromQuery] AppSettingGetAllQueryRequest request)
        {
          IList< AppSettingGetAllQueryResponse> responses = await Mediator.Send(request);
            return Ok(responses);
        }
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CreateAppSettingCommandRequest request)
        {
            CreateAppSettingCommandResponse response = await Mediator.Send(request);
            return Ok(response);
        }
        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] UpdateAppSettingsCommandRequest request)
        {
            UpdateAppSettingsCommandResponse response = await Mediator.Send(request);
            return Ok(response);
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([FromQuery] DeleteAppSettingCommandRequest request)
        {
            DeleteAppSettingCommandResponse response = await Mediator.Send(request);
            return Ok(response);
        }
    }
    
}
