using Application.Services.Repositories;
using Core.Application.Pipelines.Authorization;
using Core.Security.Constants;
using Domain.Entities;
using MediatR;

namespace Application.Features.Panel.Queries.ActiveOrPasifUsers
{
    public class UserTotalPasifAndActiveQueryHandler : IRequestHandler<UserTotalPasifAndActiveQueryRequest, UserTotalPasifAndActiveQueryResponse>
    {

        readonly IUserRepository _repository;

        public UserTotalPasifAndActiveQueryHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<UserTotalPasifAndActiveQueryResponse> Handle(UserTotalPasifAndActiveQueryRequest request, CancellationToken cancellationToken)
        {
            ICollection<User>? users = await _repository.GetListAsync();
            if (users == null) return new();
            return new UserTotalPasifAndActiveQueryResponse()
            {
                ActiveUser = users.Where(x => x.Active != false).Count(),
                PasifUser = users.Where(x => x.Active == false).Count(),
                TotalUser = users.Count(),
            };


        }
    }
}
