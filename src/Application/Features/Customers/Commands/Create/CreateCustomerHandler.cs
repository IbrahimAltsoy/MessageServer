using Application.Abstract.Common;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Domain.Events;
using MediatR;

namespace Application.Features.Customers.Commands.Create
{
    public class CreateCustomerHandler : IRequestHandler<CreateCustomerRequest, CreateCustomerResponse>
    {
        readonly ICustomerRepository _customerRepository;
        readonly IMapper _mapper;
        readonly IUser _currentUser;

        public CreateCustomerHandler(ICustomerRepository customerRepository, IMapper mapper,IUser currentUser)
        {
            _customerRepository = customerRepository;
            _currentUser = currentUser;
            _mapper = mapper;
        }

        public async Task<CreateCustomerResponse> Handle(CreateCustomerRequest request, CancellationToken cancellationToken)
        {

            var entity = _mapper.Map<Customer>(request);

            // _currentUser.Id'nin geçerli bir Guid olup olmadığını kontrol et
            if (!Guid.TryParse(_currentUser.Id, out Guid userId))
            {
                throw new ArgumentException("Current user ID is not a valid GUID.");
            }

            entity.UserId = userId;

            var result = await _customerRepository.AddAsync(entity);            
            entity.AddDomainEvent(new CustomerCreatedEvent(entity));

            // Sms Atılacak

            return new()
            {
                Message = $"Notunuz başarıyla iletildi: {result.Description ?? ""}",
            };
        }
    }
}
