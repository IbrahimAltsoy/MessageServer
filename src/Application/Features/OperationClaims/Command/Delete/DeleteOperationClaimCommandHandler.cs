using Application.Services.Repositories;
using AutoMapper;
using MediatR;

namespace Application.Features.OperationClaims.Command.Delete
{
    public class DeleteOperationClaimCommandHandler : IRequestHandler<DeleteOperationClaimCommandRequest, DeleteOperationClaimCommandResponse>
    {
        readonly IMapper _mapper;
        readonly IOperationClaimRepository _repository;
        

        public DeleteOperationClaimCommandHandler(IMapper mapper, IOperationClaimRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<DeleteOperationClaimCommandResponse> Handle(DeleteOperationClaimCommandRequest request, CancellationToken cancellationToken)
        {
            var data = await _repository.GetAsync(x=>x.Id==request.Id, cancellationToken: cancellationToken);
            if (data == null) return new();
            var deletedData = await _repository.DeleteAsync(data, permanent: true);
            var response = _mapper.Map<DeleteOperationClaimCommandResponse>(deletedData);
            return response;
        }
    }
}
