using Application.Features.Customers.Commands.Create;
using Application.Features.Customers.Commands.Delete;
using Application.Features.Customers.Queries.CustomerGetAllByUser;
using Application.Features.Customers.Queries.CustomerGetById;
using Application.Features.Customers.Queries.DeleteCustomerById;
using Application.Features.Customers.Queries.DeleteCustomerGetAll;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : BaseController
    {
        [HttpGet("Customer")]
        public async Task<IActionResult> Customer([FromQuery] CustomerGetByIdQueryRequest request)
        {
            CustomerGetByIdQueryResponse response = await Mediator.Send(request);
            return Ok(response);
        }
        
        [HttpGet("Customers")]
        public async Task<IActionResult> Customers([FromQuery] CustomerGetAllByUserQueryRequest request)
        {
            IList<CustomerGetAllByUserQueryResponse> response = await Mediator.Send(request);
            return Ok(response);
        }
        [HttpGet("CustomerDelete")]
        public async Task<IActionResult> CustomerDelete([FromQuery] DeleteCustomerByIdQueryRequest request)
        {
            CustomerGetByIdQueryResponse response = await Mediator.Send(request);
            return Ok(response);
        }
        [HttpGet("CustomersDelete")]
        public async Task<IActionResult> CustomersDelete([FromQuery] DeleteCustomerGetAllQueryRequest request)
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
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([FromQuery]CustomerDeleteCommandRequest request)
        {
            CustomerDeleteCommandResponse response = await Mediator.Send(request);
            return Ok(response);
        }

    }
}
