
using Domain.Entities;

namespace Application.Services.OperationClaimService
{
    public interface IOperationClaimServices
    {
      public Task RegisterUserSetUserClaimAsync(User user);
    }
}
