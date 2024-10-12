using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.AppSettings.Commands.Delete
{
    public class DeleteAppSettingCommandHandler : IRequestHandler<DeleteAppSettingCommandRequest, DeleteAppSettingCommandResponse>
    {
        readonly IMapper _mapper;
        readonly IAppSettingRepository _repository;

        public DeleteAppSettingCommandHandler(IMapper mapper, IAppSettingRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<DeleteAppSettingCommandResponse> Handle(DeleteAppSettingCommandRequest request, CancellationToken cancellationToken)
        {
            AppSetting? data = await _repository.GetAsync(x=>x.Id== request.Id);
            if (data != null)
            {
                var deletedData = await _repository.DeleteAsync(data,true);
                DeleteAppSettingCommandResponse response = _mapper.Map<DeleteAppSettingCommandResponse>(deletedData);
                return response;
            }
            else return new();
        }
    }
}
