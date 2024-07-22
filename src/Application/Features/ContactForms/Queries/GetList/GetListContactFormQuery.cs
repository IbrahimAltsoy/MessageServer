using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;

namespace Application.Features.ContactForms.Queries.GetList;

public class GetListContactFormQuery : IRequest<GetListResponse<GetListContactFormListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListContactRequestQueryHandler : IRequestHandler<GetListContactFormQuery, GetListResponse<GetListContactFormListItemDto>>
    {
        private readonly IMapper _mapper;
        private readonly IContactFormRepository _contactFormRepository;

        public GetListContactRequestQueryHandler(IMapper mapper, IContactFormRepository contactFormRepository)
        {
            _mapper = mapper;
            _contactFormRepository = contactFormRepository;
        }

        public async Task<GetListResponse<GetListContactFormListItemDto>> Handle(GetListContactFormQuery request, CancellationToken cancellationToken)
        {
            IPaginate<ContactForm> getList = await _contactFormRepository.GetPaginateListAsync(
                size: request.PageRequest.PageSize,
                index: request.PageRequest.Page,
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListContactFormListItemDto> mappedList = _mapper.Map<GetListResponse<GetListContactFormListItemDto>>(getList);
            return mappedList;
        }
    }
}
