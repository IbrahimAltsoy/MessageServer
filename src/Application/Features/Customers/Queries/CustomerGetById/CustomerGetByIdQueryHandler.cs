using Application.Abstract.Common;
using Application.Services.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Customers.Queries.CustomerGetById
{
    internal class CustomerGetByIdQueryHandler : IRequestHandler<CustomerGetByIdQueryRequest, CustomerGetByIdQueryResponse>
    {
        readonly IUser _currentUser;
        readonly ICustomerRepository _customerRepository;
        readonly IMapper _mapper;

        public CustomerGetByIdQueryHandler(IUser currentUser, ICustomerRepository customerRepository, IMapper mapper)
        {
            _currentUser = currentUser;
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<CustomerGetByIdQueryResponse> Handle(CustomerGetByIdQueryRequest request, CancellationToken cancellationToken)
        {
            if (!Guid.TryParse(_currentUser.Id, out Guid userId))
            {
                throw new ArgumentException("Current user ID is not a valid GUID.");
            }
            var customer =await _customerRepository.GetAsync(x =>x.UserId==userId && x.Id == request.Id, include: c => c.Include(x => x.CustomerPhotos!));
            var mappedData = _mapper.Map<CustomerGetByIdQueryResponse>(customer);
            return mappedData;
           
        }
    }
}
