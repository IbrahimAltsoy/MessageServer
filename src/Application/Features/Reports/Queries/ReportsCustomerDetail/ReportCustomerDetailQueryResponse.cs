namespace Application.Features.Reports.Queries.ReportsCustomerDetail
{
    public class ReportCustomerDetailQueryResponse
    {
        public string Message { get; set; }
        public IList<CustomerDetail> Customers { get; set; }
        public class CustomerDetail
        {
            //public Guid CustomerId { get; set; }
            public string NameSurname { get; set; }
            public string Phone {  get; set; }
           
        }
    }
}
