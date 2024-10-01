using Application.Features.Customers.Queries.CustomerGetAllByUser;
using Domain.Enums;
using SmartVisitServer.Web.Models.Paginate;

namespace SmartVisitServer.Web.Services.Customers
{
    public interface ICustomerService
    {
        Task<List<CustomerGetAllByUserQueryResponse>> GetCustomersAsync(int page, int pageSize, TimePeriodType? periodType);
    }
}
