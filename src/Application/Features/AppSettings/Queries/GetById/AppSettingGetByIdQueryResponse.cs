namespace Application.Features.AppSettings.Queries.GetById
{
    public class AppSettingGetByIdQueryResponse
    {
        public Guid Id { get; set; }
        public string Value {  get; set; }
        public string Key {  get; set; }
    }
}
