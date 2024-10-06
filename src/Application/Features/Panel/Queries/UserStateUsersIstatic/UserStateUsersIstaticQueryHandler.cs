using Application.Services.Repositories;
using Application.Services.UserService;
using Domain.Enums;
using MediatR;

namespace Application.Features.Panel.Queries.UserStateUsersIstatic
{
    public class UserStateUsersIstaticQueryHandler : IRequestHandler<UserStateUsersIstaticQueryRequest, UserStateUsersIstaticQueryResponse>
    {
        readonly IUserService _userService;

        public UserStateUsersIstaticQueryHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<UserStateUsersIstaticQueryResponse> Handle(UserStateUsersIstaticQueryRequest request, CancellationToken cancellationToken)
        {
            var model = await _userService.UserStateUsersIstaticQueryResponseAsync();
            return model;
        }
        
    }
}
