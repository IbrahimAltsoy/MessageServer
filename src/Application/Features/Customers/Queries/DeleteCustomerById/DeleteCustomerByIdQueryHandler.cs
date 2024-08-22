using Application.Abstract.Common;
using Application.Features.Customers.Queries.CustomerGetById;
using Application.Services.Repositories;
using AutoMapper;
using MediatR;

namespace Application.Features.Customers.Queries.DeleteCustomerById
{
    public class DeleteCustomerByIdQueryHandler : IRequestHandler<DeleteCustomerByIdQueryRequest, CustomerGetByIdQueryResponse>
    {
        readonly IUser _currentUser;
        readonly ICustomerRepository _customerRepository;
        readonly IMapper _mapper;

        public DeleteCustomerByIdQueryHandler(IUser currentUser, ICustomerRepository customerRepository, IMapper mapper)
        {
            _currentUser = currentUser;
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<CustomerGetByIdQueryResponse> Handle(DeleteCustomerByIdQueryRequest request, CancellationToken cancellationToken)
        {
            if (!Guid.TryParse(_currentUser.Id, out Guid userId))
            {
                throw new ArgumentException("Current user ID is not a valid GUID.");
            }
            var customer = await _customerRepository.GetAsync(x => x.UserId == userId && x.Id == request.Id && x.DeletedDate!=null, withDeleted:true );
            var mappedData = _mapper.Map<CustomerGetByIdQueryResponse>(customer);
            return mappedData;
        }
    }
}
