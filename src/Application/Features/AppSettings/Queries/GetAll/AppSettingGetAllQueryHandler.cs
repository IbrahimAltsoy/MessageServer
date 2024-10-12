using Application.Abstract.Common;
using Application.Features.Customers.Queries.CustomerGetAllByUser;
using Application.Helpers;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Features.AppSettings.Queries.GetAll
{
    public class AppSettingGetAllQueryHandler : IRequestHandler<AppSettingGetAllQueryRequest, IList<AppSettingGetAllQueryResponse>>
    {
        readonly IMapper _mapper;
        readonly IAppSettingRepository _appSettingRepository;
        

        public AppSettingGetAllQueryHandler(IMapper mapper, IAppSettingRepository appSettingRepository)
        {
            _mapper = mapper;
            _appSettingRepository = appSettingRepository;
           
        }

        public async Task<IList<AppSettingGetAllQueryResponse>> Handle(AppSettingGetAllQueryRequest request, CancellationToken cancellationToken)
        {                    
            ICollection<AppSetting> datas = await _appSettingRepository.GetListAsync();
            IList<AppSettingGetAllQueryResponse> responses = _mapper.Map<List<AppSettingGetAllQueryResponse>>(datas).ToList();            
            return responses;
        }
    }
}
