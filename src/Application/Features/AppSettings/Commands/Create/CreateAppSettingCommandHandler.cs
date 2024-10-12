using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.AppSettings.Commands.Create
{
    public class CreateAppSettingCommandHandler : IRequestHandler<CreateAppSettingCommandRequest, CreateAppSettingCommandResponse>
    {
        readonly IMapper _mapper;
        readonly IAppSettingRepository _repository;

        public CreateAppSettingCommandHandler(IMapper mapper, IAppSettingRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<CreateAppSettingCommandResponse> Handle(CreateAppSettingCommandRequest request, CancellationToken cancellationToken)
        {
            var data = _mapper.Map<AppSetting>(request);
            var createdData = await _repository.AddAsync(data);
            CreateAppSettingCommandResponse response = _mapper.Map<CreateAppSettingCommandResponse>(createdData);
            return response;
        }
    }
}
