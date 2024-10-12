namespace Application.Features.AppSettings.Commands.Create
{
    public class CreateAppSettingCommandResponse
    {
        public Guid Id { get; set; }
        public string Value { get; set; }
        public string Key { get; set; }
    }
}
