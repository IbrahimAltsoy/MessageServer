using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace Application.Features.Panel.Queries.UserMemberShipLastDay
{
    public class UserMemberShipLastDayQueryHandler : IRequestHandler<UserMemberShipLastDayQueryRequest, GetListResponse<UserMemberShipLastDayQueryResponse>>
    {
        readonly IUserRepository _repository;        
        readonly IMapper _mapper;

        public UserMemberShipLastDayQueryHandler(IUserRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<UserMemberShipLastDayQueryResponse>> Handle(UserMemberShipLastDayQueryRequest request, CancellationToken cancellationToken)
        {
            var nextWeek = DateTime.UtcNow.AddDays(7);
            IPaginate<User> datas = await _repository.GetPaginateListAsync(x => x.Membership.LastDay.HasValue && x.Membership.LastDay.Value >= DateTime.UtcNow && x.Membership.LastDay.Value <= nextWeek, include: x=>x.Include(x=>x.Membership), orderBy: x=>x.OrderBy(x=>x.Membership.LastDay), size: request.PageRequest.PageSize,
                index: request.PageRequest.Page);
            IList<UserMemberShipLastDayQueryResponse> data = _mapper.Map<IList<UserMemberShipLastDayQueryResponse>>(datas.Items).ToList();
            GetListResponse<UserMemberShipLastDayQueryResponse> responses = new GetListResponse<UserMemberShipLastDayQueryResponse>()
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
