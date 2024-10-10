using Application.Features.Customers.Queries.CustomerGetAllByUser;
using Core.Application.Responses;
using Domain.Enums;


namespace SmartVisitServer.Web.Services.Customers
{
    public interface ICustomerService
    {
        Task<GetListResponse<CustomerGetAllByUserQueryResponse>> GetCustomersAsync(int page, int pageSize, TimePeriodType? periodType);
        Task<GetListResponse<CustomerGetAllByUserQueryResponse>> CustomerSearchByPhoneOrNameSurnameAsync(string searchTerm);
    }
}
