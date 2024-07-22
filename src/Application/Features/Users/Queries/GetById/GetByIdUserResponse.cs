using Core.Application.Responses;

namespace Application.Features.Users.Queries.GetById;

public class GetByIdUserResponse : IResponse
{
    public Guid? Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public bool VerifyEmail { get; set; }
    public string Phone { get; set; }
    public bool VerifyPhone { get; set; }
    public bool Status { get; set; }
}
