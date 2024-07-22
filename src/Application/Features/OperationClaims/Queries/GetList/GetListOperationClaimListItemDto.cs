using Core.Application.Dtos;

namespace Application.Features.OperationClaims.Queries.GetList;

public class GetListOperationClaimListItemDto : IDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}
