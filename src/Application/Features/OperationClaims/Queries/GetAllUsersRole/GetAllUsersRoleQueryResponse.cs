using Domain.Enums;

namespace Application.Features.OperationClaims.Queries.GetAllUsersRole
{
    public class GetAllUsersRoleQueryResponse
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? CompanyName { get; set; }
        public string? Phone { get; set; }
        public string Email { get; set; }
        public UserStatus UserStatus { get; set; }

        public string RoleName { get; set; }
    }
}
