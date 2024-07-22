using Domain.AdapterModels;

namespace Application.Services.PublicHolidaysService;

public interface IPublicHolidaysService
{
    Task<ResultPublicHolidays> PublicHolidaysAsync();
}
