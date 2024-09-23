using Application.Features.Customers.Queries.CustomerGetAllByUser;
using Core.Application.Requests;
using Domain.Enums;
using MediatR;

namespace Application.Features.Customers.Queries.DeleteCustomerGetAll
{
    public class DeleteCustomerGetAllQueryRequest:IRequest<IList<CustomerGetAllByUserQueryResponse>>
    {
        public TimePeriodType? TimePeriod { get; set; } = TimePeriodType.Daily;
        public PageRequest PageRequest { get; set; }
        
    }
}
