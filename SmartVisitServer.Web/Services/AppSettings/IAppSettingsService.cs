using Application.Features.AppSettings.Commands.Create;
using Application.Features.AppSettings.Commands.Delete;
using Application.Features.AppSettings.Commands.Update;
using Application.Features.AppSettings.Queries.GetAll;
using Application.Features.AppSettings.Queries.GetById;

namespace SmartVisitServer.Web.Services.AppSettings
{
    public interface IAppSettingsService
    {
        Task<AppSettingGetAllQueryResponse> GetAllAppSettingGetAllQueryAsync();
        Task<AppSettingGetByIdQueryResponse> GetAppSettingGetByIdAsync(Guid id);
        Task<CreateAppSettingCommandResponse> AppSettingCreateAsync(CreateAppSettingCommandRequest request);
        Task<UpdateAppSettingsCommandResponse> AppSettingUpdateAsync(UpdateAppSettingsCommandRequest request);
        Task<DeleteAppSettingCommandResponse> AppSettingDeleteAsync(Guid id);
    }
}
