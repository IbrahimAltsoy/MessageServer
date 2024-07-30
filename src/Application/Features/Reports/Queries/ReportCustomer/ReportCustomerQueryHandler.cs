using Application.Abstract.Common;
using Application.Features.Reports.Queries.ReportSms;
using Application.Helpers;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Features.Reports.Queries.ReportCustomer
{
    public class ReportCustomerQueryHandler : IRequestHandler<ReportCustomerQueryRequest, ReportCustomerQueryResponse>
    {
        readonly ICustomerRepository _customerRepository;
        readonly IUser _currentUser;

        public ReportCustomerQueryHandler(ICustomerRepository customerRepository, IUser currentUser)
        {
            _customerRepository = customerRepository;
            _currentUser = currentUser;
        }

        public async Task<ReportCustomerQueryResponse> Handle(ReportCustomerQueryRequest request, CancellationToken cancellationToken)
        {
            if (!Guid.TryParse(_currentUser.Id, out Guid userId))
            {
                throw new ArgumentException("Current user ID is not a valid GUID.");
            }
            var (startDate, endDate) = DateRangeHelper.GetDateRange(request.TimePeriod ?? TimePeriodType.Daily);

            IPaginate<Customer> datas = await _customerRepository.GetPaginateListAsync(
               predicate: s => s.CreatedDate >= startDate && s.CreatedDate <= endDate && s.UserId == userId,
                cancellationToken: cancellationToken
            );
            string formattedStartDate = startDate.ToString("dd.MM.yyyy");
            string formattedEndDate = DateTime.UtcNow.ToString("dd.MM.yyyy");
            return new ReportCustomerQueryResponse()
            {

                Message = $"{formattedStartDate}- {formattedEndDate} tarihleri arasında {datas.Count} müşteri kaydı oluşturuldu."
            };
        }
    }
}
