using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.ContactForms.Commands.Delete;

public class DeleteContactFormCommand : IRequest
{
    public Guid Id { get; set; }

    public class DeleteContactFormCommandHandler : IRequestHandler<DeleteContactFormCommand>
    {
        private readonly IMapper _mapper;
        private readonly IContactFormRepository _contactFormRepository;

        public DeleteContactFormCommandHandler(IMapper mapper, IContactFormRepository contactFormRepository)
        {
            _mapper = mapper;
            _contactFormRepository = contactFormRepository;
        }

        public async Task Handle(DeleteContactFormCommand request, CancellationToken cancellationToken)
        {
            ContactForm mappedForm = _mapper.Map<ContactForm>(request);
            await _contactFormRepository.DeleteAsync(mappedForm);
            await Task.CompletedTask;
        }
    }
}