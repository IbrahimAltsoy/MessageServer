using Application.Features.Panel.Queries.UserMemberShipLastDay;
using Application.Services.Repositories;
using Application.Services.UserService;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;

namespace Application.Features.Panel.Queries.CreatedCompanyLastMontly
{
    public class CreatedCompanyLastMontlyQueryHandler : IRequestHandler<CreatedCompanyLastMontlyQueryRequest, GetListResponse<CreatedCompanyLastMontlyQueryResponse>>
    {
        readonly IUserRepository _repository;
        readonly IUserService _userService;
        readonly IMapper _mapper;

        public CreatedCompanyLastMontlyQueryHandler(IUserRepository repository, IMapper mapper, IUserService userService)
        {
            _repository = repository;
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<GetListResponse<CreatedCompanyLastMontlyQueryResponse>> Handle(CreatedCompanyLastMontlyQueryRequest request, CancellationToken cancellationToken)
        {           
            IPaginate<User> datas = await _repository.GetPaginateListAsync(x => x.CreatedDate >= DateTime.UtcNow.AddMonths(-1) && (x.UserStatus==Domain.Enums.UserStatus.Active || x.UserStatus==Domain.Enums.UserStatus.Passive), orderBy: x => x.OrderBy(x => x.CreatedDate), size: request.PageRequest.PageSize,
                index: request.PageRequest.Page);
            var updatedItems = datas.Items.Select(item =>
            {
                item.QRCode = _userService.SaveQRCodeImage(item.QRCode);
                return item; 
            }).ToList();

            IList<CreatedCompanyLastMontlyQueryResponse> data = _mapper.Map<IList<CreatedCompanyLastMontlyQueryResponse>>(datas.Items).ToList();
            GetListResponse<CreatedCompanyLastMontlyQueryResponse> responses = new GetListResponse<CreatedCompanyLastMontlyQueryResponse>
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
