using Core.Application.Responses;

namespace Application.Features.ContactForms.Commands.ChangeStatus;

public class ChangedStatusContactFormResponse: IResponse
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string Phone { get; set; }
    public string Mail { get; set; }
    public string Description { get; set; }
    public bool Read { get; set; }
}