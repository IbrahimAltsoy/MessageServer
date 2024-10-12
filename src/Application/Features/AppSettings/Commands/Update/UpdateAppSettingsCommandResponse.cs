namespace Application.Features.AppSettings.Commands.Update
{
    public class UpdateAppSettingsCommandResponse
    {
        public Guid Id { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
