namespace Application.Services.AppSettingService
{
    public interface IAppSettingService
    {
        public Task<string> GetParameterAsync(string key);
        public Task<string> UpdateParameterAsync(string key, string value);
    }
}
