using Core.Application.Requests;
using Core.Application.Responses;
using MediatR;

namespace Application.Features.AppSettings.Queries.GetAll
{
    public class AppSettingGetAllQueryRequest:IRequest<IList<AppSettingGetAllQueryResponse>>
    {
       
    }
}
