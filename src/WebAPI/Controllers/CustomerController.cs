using Application.Features.Customers.Commands.Create;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : BaseController
    {
        [HttpPost("createCustomer")]
        public async Task<IActionResult> Create(CreateCustomerRequest request)
        {
            CreateCustomerResponse response = await Mediator.Send(request);
            return Ok(response);
        }
    }
}
