using Core.Application.Responses;

namespace Application.Features.Users.Queries.GetById;

public class GetByIdUserResponse : IResponse
{
    public Guid? Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Phone { get; set; }
    public string? CompanyName { get; set; }
    public string? IbanNumber { get; set; }
    public string? Adress { get; set; }
    public string? LogoUrl { get; set; }
}
