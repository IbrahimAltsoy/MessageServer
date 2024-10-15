namespace Application.Features.OperationClaims.Queries.GetAll
{
    public class OperationClaimGetAllQueryResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int? UserCount { get; set; } = 0;
    }
}
