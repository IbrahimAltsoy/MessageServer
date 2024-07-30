using Application.Abstract.Common;
using Application.Helpers;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Features.Reports.Queries.ReportSms
{
    public class ReportSmsQueryHandler : IRequestHandler<ReportSmsQueryRequest, ReportSmsQueryResponse>
    {
        readonly ISmsRepository _smsRepository;
        readonly IUser _currentUser;

        public ReportSmsQueryHandler(ISmsRepository smsRepository, IUser currentUser)
        {
            _smsRepository = smsRepository;
            _currentUser = currentUser;
        }

        public async Task<ReportSmsQueryResponse> Handle(ReportSmsQueryRequest request, CancellationToken cancellationToken)
        {
            if (!Guid.TryParse(_currentUser.Id, out Guid userId))
            {
                throw new ArgumentException("Current user ID is not a valid GUID.");
            }
            var (startDate, endDate) = DateRangeHelper.GetDateRange(request.TimePeriod ?? TimePeriodType.Daily);

            IPaginate<Sms> datas = await _smsRepository.GetPaginateListAsync(
               predicate: s => s.CreatedDate >= startDate && s.CreatedDate <= endDate &&  s.UserId == userId,
                cancellationToken: cancellationToken
            );
            string formattedStartDate = startDate.ToString("dd.MM.yyyy");
            string formattedEndDate = DateTime.UtcNow.ToString("dd.MM.yyyy");
            return new ReportSmsQueryResponse()
            {
                
                Message= $"{formattedStartDate}- {formattedEndDate} tarihleri arasında {datas.Count} sms gönderildi."
            };
           
        }
    }
}
