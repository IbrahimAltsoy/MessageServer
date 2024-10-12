using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.OperationClaims.Queries.GetAllUsersRole
{
    public class GetAllUsersRoleQueryHandler : IRequestHandler<GetAllUsersRoleQueryRequest, GetListResponse<GetAllUsersRoleQueryResponse>>
    {
        readonly IUserRepository _userRepository;
        readonly IMapper _mapper;

        public GetAllUsersRoleQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetAllUsersRoleQueryResponse>> Handle(GetAllUsersRoleQueryRequest request, CancellationToken cancellationToken)
        {
            IPaginate<User> datas = await _userRepository.GetPaginateListAsync(size: request.PageRequest.PageSize,
                index: request.PageRequest.Page,
                include: x => x.Include(u => u.UserOperationClaims)
                  .ThenInclude(uoc => uoc.OperationClaim)
                );

            IList< GetAllUsersRoleQueryResponse > data = _mapper.Map<IList< GetAllUsersRoleQueryResponse >>(datas.Items).ToList();
            GetListResponse<GetAllUsersRoleQueryResponse> responses = new GetListResponse<GetAllUsersRoleQueryResponse>
            {
                Index = datas.Index,
                Size = datas.Size,
                Count = datas.Count,
                Pages = datas.Pages,
                HasPrevious = datas.HasPrevious,
                HasNext = datas.HasNext,
                Items = data,
            };
            return responses;
        }
    }
}
