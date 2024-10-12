using Application.Features.AppSettings.Commands.Create;
using Application.Features.AppSettings.Commands.Delete;
using Application.Features.AppSettings.Commands.Update;
using Application.Features.AppSettings.Queries.GetAll;
using Application.Features.AppSettings.Queries.GetById;
using AutoMapper;
using Domain.Entities;

namespace Application.Features.AppSettings
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<AppSetting, AppSettingGetByIdQueryResponse>().ReverseMap();
            CreateMap<AppSetting, AppSettingGetAllQueryResponse>().ReverseMap();

            CreateMap<AppSetting, CreateAppSettingCommandRequest>().ReverseMap();
            CreateMap<AppSetting, CreateAppSettingCommandResponse>().ReverseMap();

            CreateMap<AppSetting, UpdateAppSettingsCommandResponse>().ReverseMap();
            CreateMap<AppSetting, UpdateAppSettingsCommandRequest>().ReverseMap();

            CreateMap<AppSetting, DeleteAppSettingCommandResponse>().ReverseMap();

        }
    }
}
