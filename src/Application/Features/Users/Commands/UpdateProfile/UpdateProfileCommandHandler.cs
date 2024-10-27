using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Users.Commands.UpdateProfile
{
    public class UpdateProfileCommandHandler : IRequestHandler<UpdateProfileCommandRequest, UpdateProfileCommandResponse>
    {
        readonly IUserRepository _userRepository;
        readonly IMapper _mapper;

        public UpdateProfileCommandHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UpdateProfileCommandResponse> Handle(UpdateProfileCommandRequest request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetAsync(x=>x.Id== request.Id);
            _mapper.Map(request, user);
            var data = _mapper.Map<User>(user);
            var updateData = await _userRepository.UpdateAsync(data);
            var response = _mapper.Map<UpdateProfileCommandResponse>(updateData);
            return response;
        }
    }
}
