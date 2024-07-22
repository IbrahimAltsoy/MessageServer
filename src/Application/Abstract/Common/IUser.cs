using System.Security.Claims;

namespace Application.Abstract.Common
{
    public interface IUser
    {
        public string? Id { get;  }
        string Name { get; }
        IEnumerable<Claim>? Claims { get; }
    }
}
