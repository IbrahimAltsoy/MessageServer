using Core.Application.Responses;
using Domain.Enums;

namespace Application.Features.ContactForms.Queries.GetById;

public class GetByIdContactFormResponse : IResponse
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string Phone { get; set; }
    public string Mail { get; set; }
    public string Description { get; set; }
    public bool Read { get; set; }
}
