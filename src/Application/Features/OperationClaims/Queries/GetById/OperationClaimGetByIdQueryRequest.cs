using MediatR;

namespace Application.Features.OperationClaims.Queries.GetById
{
    public class OperationClaimGetByIdQueryRequest:IRequest<OperationClaimGetByIdQueryResponse>
    {
        public Guid Id { get; set; }
    }
}
