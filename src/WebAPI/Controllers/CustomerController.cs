using Application.Features.Customers.Commands.Create;
using Application.Features.Customers.Queries.CustomerGetAllByUser;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : BaseController
    {
        [HttpGet("getCustomers")]
        public async Task<IActionResult> GetCustomers([FromQuery] CustomerGetAllByUserQueryRequest request)
        {
            IList<CustomerGetAllByUserQueryResponse> response = await Mediator.Send(request);
            return Ok(response);
        }
        [HttpPost("createCustomer")]
        public async Task<IActionResult> Create(CreateCustomerRequest request)
        {
            CreateCustomerResponse response = await Mediator.Send(request);
            return Ok(response);
        }

    }
}
