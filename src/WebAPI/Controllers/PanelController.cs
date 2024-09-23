using Application.Features.Panel.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PanelController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> UserTotalPasifAndActive([FromQuery]UserTotalPasifAndActiveQueryRequest request)
        {
            UserTotalPasifAndActiveQueryResponse response = await Mediator.Send(request);
            return Ok(response);
        }
    }
}
