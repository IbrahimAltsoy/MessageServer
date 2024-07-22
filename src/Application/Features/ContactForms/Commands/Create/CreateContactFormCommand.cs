using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.ContactForms.Commands.Create;

public class CreateContactFormCommand : IRequest
{
    public string FullName { get; set; }
    public string Phone { get; set; }
    public string Mail { get; set; }
    public string Description { get; set; }

    public class CreateContactFormCommandHandler : IRequestHandler<CreateContactFormCommand>
    {
        private readonly IMapper _mapper;
        private readonly IContactFormRepository _contactFormRepository;

        public CreateContactFormCommandHandler(IMapper mapper, IContactFormRepository contactFormRepository)
        {
            _mapper = mapper;
            _contactFormRepository = contactFormRepository;
        }

        public async Task Handle(CreateContactFormCommand request, CancellationToken cancellationToken)
        {
            ContactForm mappedForm = _mapper.Map<ContactForm>(request);
            await _contactFormRepository.AddAsync(mappedForm);
            await Task.CompletedTask;
        }
    }
}
