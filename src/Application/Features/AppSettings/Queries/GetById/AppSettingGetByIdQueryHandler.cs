using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.AppSettings.Queries.GetById
{
    public class AppSettingGetByIdQueryHandler : IRequestHandler<AppSettingGetByIdQueryRequest, AppSettingGetByIdQueryResponse>
    {
        readonly IMapper _mapper;
        readonly IAppSettingRepository _repository;

        public AppSettingGetByIdQueryHandler(IMapper mapper, IAppSettingRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<AppSettingGetByIdQueryResponse> Handle(AppSettingGetByIdQueryRequest request, CancellationToken cancellationToken)
        {
            AppSetting? data = await _repository.GetAsync(x=>x.Id==request.Id);
            if (data != null)
            {
                AppSettingGetByIdQueryResponse response = _mapper.Map<AppSettingGetByIdQueryResponse>(data);
                return response;
            }
            else return new();
        }
    }
}
