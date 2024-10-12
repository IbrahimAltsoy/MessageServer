using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.AppSettings.Commands.Update
{
    public class UpdateAppSettingsCommandHandler : IRequestHandler<UpdateAppSettingsCommandRequest, UpdateAppSettingsCommandResponse>
    {
        readonly IMapper _mapper;
        readonly IAppSettingRepository _repository;

        public UpdateAppSettingsCommandHandler(IMapper mapper, IAppSettingRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<UpdateAppSettingsCommandResponse> Handle(UpdateAppSettingsCommandRequest request, CancellationToken cancellationToken)
        {
            AppSetting? data = await _repository.GetAsync(x => x.Id == request.Id);
            if (data == null) return new();
            else
            {
                data.Value = request.Value;
                data.Key= request.Key;
            }
           var updateData = await _repository.UpdateAsync(data);
            UpdateAppSettingsCommandResponse response = _mapper.Map<UpdateAppSettingsCommandResponse>(updateData);
            return response;
        }
    }
}
