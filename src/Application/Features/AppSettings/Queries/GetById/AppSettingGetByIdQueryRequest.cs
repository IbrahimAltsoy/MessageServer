using MediatR;

namespace Application.Features.AppSettings.Queries.GetById
{
    public class AppSettingGetByIdQueryRequest:IRequest<AppSettingGetByIdQueryResponse>
    {
        public Guid Id { get; set; }
    }
}
