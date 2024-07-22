using Core.Application.Dtos;
using Domain.Enums;

namespace Application.Features.ContactForms.Queries.GetList;

public class GetListContactFormListItemDto : IDto
{
    public Guid Id { get; set; }
    public string FullName { get; set; }
    public string Phone { get; set; }
    public string Mail { get; set; }
    public string Description { get; set; }
    public bool Read { get; set; }
}
