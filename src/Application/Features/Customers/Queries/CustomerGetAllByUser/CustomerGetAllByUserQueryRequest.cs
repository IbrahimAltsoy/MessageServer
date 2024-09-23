using Core.Application.Pipelines.Authorization;
using Core.Application.Requests;
using Core.Security.Constants;
using Domain.Enums;
using MediatR;

namespace Application.Features.Customers.Queries.CustomerGetAllByUser
{
    public class CustomerGetAllByUserQueryRequest:IRequest<IList<CustomerGetAllByUserQueryResponse>>/*,ISecuredRequest*/
    {
        public TimePeriodType? TimePeriod { get; set; } = TimePeriodType.Daily;
        public PageRequest PageRequest { get; set; }
        //public string[] Roles => new[] { GeneralOperationClaims.Admin };
    }
}
