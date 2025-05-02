namespace Application.Features.Customers.Queries.Summary
{
    public class CustomerSummaryQueryResponse
    {
        public int Delivered { get; set; }
        public int Waiting { get; set; }
        public int Canceled { get; set; }
    }
}
