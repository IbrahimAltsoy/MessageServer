using MediatR;

namespace Application.Features.OperationClaims.Command.Update
{
    public class UpdateOperationClaimCommandRequest:IRequest<UpdateOperationClaimCommandResponse>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
