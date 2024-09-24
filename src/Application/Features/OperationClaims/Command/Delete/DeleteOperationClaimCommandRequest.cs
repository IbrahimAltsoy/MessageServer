using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.OperationClaims.Command.Delete
{
    public class DeleteOperationClaimCommandRequest:IRequest<DeleteOperationClaimCommandResponse>
    {
        public Guid Id { get; set; }
    }
}
