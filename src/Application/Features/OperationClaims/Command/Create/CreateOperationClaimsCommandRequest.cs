using MediatR;

namespace Application.Features.OperationClaims.Command.Create
{
    public class CreateOperationClaimsCommandRequest:IRequest<CreateOperationClaimsCommandResponse>
    {
        public string Name { get; set; }
    }
}
