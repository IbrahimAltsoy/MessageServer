using Domain.Enums;

namespace Application.Features.Users.Commands.UpdateUserRole
{
    public class UpdateUserRoleCommandResponse
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? CompanyName { get; set; }
        public string? Phone { get; set; }
        public string Email { get; set; }
        public UserStatus UserStatus { get; set; }

        public string RoleName { get; set; }
    }
}
