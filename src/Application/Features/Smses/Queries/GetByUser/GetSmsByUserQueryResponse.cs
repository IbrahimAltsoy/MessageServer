namespace Application.Features.Smses.Queries.GetByUser
{
    public class GetSmsByUserQueryResponse
    {
        public Guid Id { get; set; }
        public string NameSurname { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public string Phone { get; set; }


        public string Content { get; set; }
        //public Guid? CustomerId { get; set; }
       
    }
}
