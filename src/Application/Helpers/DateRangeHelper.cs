using Domain.Enums;

namespace Application.Helpers
{
    public static class DateRangeHelper
    {
        public static (DateTime startDate, DateTime endDate) GetDateRange()
        {
            DateTime startDate = DateTime.UtcNow;
            DateTime endDate = DateTime.UtcNow;


            return (startDate, endDate);
        }
    }

}
