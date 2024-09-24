using Application.Services.AppSettingService;
using Application.Services.Repositories;
using Domain.Entities;

namespace Persistence.Services.AppSettingServices
{
    public class AppSettingService : IAppSettingService
    {
        readonly IAppSettingRepository _appSettingRepository;

        public AppSettingService(IAppSettingRepository appSettingRepository)
        {
            _appSettingRepository = appSettingRepository;
        }

        public async Task<string> GetParameterAsync(string key)
        {
            AppSetting? appSetting = await _appSettingRepository.GetAsync(c => c.Key == key);
            if (appSetting == null) return "Ayar bulunamadı";
            return appSetting!.Value;
        }

        public async Task<string> UpdateParameterAsync(string key, string value)
        {
            AppSetting? appSetting = await _appSettingRepository.GetAsync(c => c.Key == key);
            appSetting!.Value = value;
            await _appSettingRepository.UpdateAsync(appSetting);
            return appSetting!.Value;
        }
    }
}
