using Application.Abstract.Common;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Customers.Commands.Update
{
    public class CustomerUpdateCommandHandler : IRequestHandler<CustomerUpdateCommandRequest, CustomerUpdateCommandResponse>
    {
        readonly ICustomerRepository _customerRepository;
        readonly IMapper _mapper;
        readonly IUser _currentUser;

        public CustomerUpdateCommandHandler(ICustomerRepository customerRepository, IMapper mapper, IUser currentUser)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
            _currentUser = currentUser;
        }

        public async Task<CustomerUpdateCommandResponse> Handle(CustomerUpdateCommandRequest request, CancellationToken cancellationToken)
        {
            if (!Guid.TryParse(_currentUser.Id, out Guid userId))
            {
                throw new ArgumentException("Current user ID is not a valid GUID.");
            }
            
            var data = _mapper.Map<Customer>(request);
            data.UserId = userId;
           var updateData = await _customerRepository.UpdateAsync(data);
            var mappedData = _mapper.Map<CustomerUpdateCommandResponse>(updateData);
            return mappedData;
        }
    }
}
