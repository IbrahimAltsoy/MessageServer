namespace Application.Features.AppSettings.Commands.Delete
{
    public class DeleteAppSettingCommandResponse
    {
        public Guid Id { get; set; }
        public string Value { get; set; }
        public string Key { get; set; }
    }
}
