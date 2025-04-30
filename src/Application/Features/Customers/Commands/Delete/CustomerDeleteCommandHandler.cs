using Application.Services.Repositories;
using AutoMapper;
using MediatR;

namespace Application.Features.Customers.Commands.Delete
{
    public class CustomerDeleteCommandHandler : IRequestHandler<CustomerDeleteCommandRequest, CustomerDeleteCommandResponse>
    {
        readonly ICustomerRepository _repository;
        readonly IMapper _mapper;

        public CustomerDeleteCommandHandler(ICustomerRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<CustomerDeleteCommandResponse> Handle(CustomerDeleteCommandRequest request, CancellationToken cancellationToken)
        {
            var customer = await _repository.GetAsync(x => x.Id == request.Id);
            await _repository.DeleteAsync(customer!);
            var mappedData = _mapper.Map<CustomerDeleteCommandResponse>(customer);
            return mappedData;

        }
    }
}
