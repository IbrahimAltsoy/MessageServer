using Application.Services.Repositories;
using AutoMapper;
using MediatR;

namespace Application.Features.Panel.Command.UpdateUserStatus
{
    public class UpdateUserStateHandler : IRequestHandler<UpdateUserStateRequest, UpdateUserStateResponse>
    {
        public IUserRepository _repository;
        public IMapper _mapper;

        public UpdateUserStateHandler(IUserRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<UpdateUserStateResponse> Handle(UpdateUserStateRequest request, CancellationToken cancellationToken)
        {
            var data = await _repository.GetAsync(x=>x.Id== request.Id);
            data.UserStatus = request.UserStatus;
            await _repository.UpdateAsync(data);
            var response = _mapper.Map<UpdateUserStateResponse>(data);
            return response;
        }
    }
}
