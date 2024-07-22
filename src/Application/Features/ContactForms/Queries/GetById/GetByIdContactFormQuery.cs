using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.ContactForms.Queries.GetById;

public class GetByIdContactFormQuery : IRequest<GetByIdContactFormResponse>
{
    public Guid Id { get; set; }

    public class GetByIdContactFormQueryHandler : IRequestHandler<GetByIdContactFormQuery, GetByIdContactFormResponse>
    {
        private readonly IMapper _mapper;
        private readonly IContactFormRepository _contactFormRepository;

        public GetByIdContactFormQueryHandler(IMapper mapper, IContactFormRepository contactFormRepository)
        {
            _mapper = mapper;
            _contactFormRepository = contactFormRepository;
        }

        public async Task<GetByIdContactFormResponse> Handle(GetByIdContactFormQuery request, CancellationToken cancellationToken)
        {
            ContactForm? findedForm = await _contactFormRepository.GetAsync(c => c.Id == request.Id);
            GetByIdContactFormResponse dto = _mapper.Map<GetByIdContactFormResponse>(findedForm);
            return dto;
        }
    }
}