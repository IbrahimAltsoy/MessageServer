using Application.Features.ContactForms.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.ContactForms.Commands.ChangeStatus;

public class ChangeStatusContactFormCommand : IRequest<ChangedStatusContactFormResponse>
{
    public Guid Id { get; set; }

    public class ChangeStatusContactFormCommandHandler : IRequestHandler<ChangeStatusContactFormCommand, ChangedStatusContactFormResponse>
    {
        private readonly IMapper _mapper;
        private readonly IContactFormRepository _contactFormRepository;
        private readonly ContactFormBusinessRules _rules;

        public ChangeStatusContactFormCommandHandler(IMapper mapper, IContactFormRepository contactFormRepository, ContactFormBusinessRules rules)
        {
            _mapper = mapper;
            _contactFormRepository = contactFormRepository;
            _rules = rules;
        }

        public async Task<ChangedStatusContactFormResponse> Handle(ChangeStatusContactFormCommand request, CancellationToken cancellationToken)
        {
            ContactForm? findedForm = await _contactFormRepository.GetAsync(c => c.Id == request.Id);
            // TODO: Rules
            findedForm.Read = true;
            ContactForm updatedForm = await _contactFormRepository.UpdateAsync(findedForm);
            ChangedStatusContactFormResponse dto = _mapper.Map<ChangedStatusContactFormResponse>(updatedForm);
            return dto;
        }
    }
}
