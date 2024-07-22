using Application.Services.NotificationService;
using AutoMapper;
using MediatR;

namespace Application.Features.Notificatiton.Queries.GetByAuth;

public class GetByAuthQuery : IRequest<List<GetByAuthResponse>>
{
    public Guid UserId { get; set; }

    public class GetByAuthQueryHandler : IRequestHandler<GetByAuthQuery, List<GetByAuthResponse>>
    {
        private readonly INotificationService _notificationService;
        private readonly IMapper _mapper;

        public GetByAuthQueryHandler(INotificationService notificationService, IMapper mapper)
        {
            _notificationService = notificationService;
            _mapper = mapper;
        }

        public async Task<List<GetByAuthResponse>> Handle(GetByAuthQuery request, CancellationToken cancellationToken)
        {
            var list = await _notificationService.GetNotificationsAsync(request.UserId);
            List<GetByAuthResponse> listData = _mapper.Map<List<GetByAuthResponse>>(list);
            return listData;
        }
    }
}
