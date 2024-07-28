using Domain.Enums;

namespace Application.Helpers
{
    public static class DateRangeHelper
    {
        public static (DateTime startDate, DateTime endDate) GetDateRange(TimePeriodType timePeriod)
        {
            DateTime startDate;
            DateTime endDate;

            switch (timePeriod)
            {
                case TimePeriodType.Daily:
                    startDate = DateTime.SpecifyKind(DateTime.UtcNow.Date, DateTimeKind.Utc);
                    endDate = DateTime.SpecifyKind(startDate.AddDays(1).AddTicks(-1), DateTimeKind.Utc);
                    break;
                case TimePeriodType.Weekly:
                    // Haftanın başlangıç gününü Pazartesi kabul edelim
                    int currentDayOfWeek = (int)DateTime.UtcNow.DayOfWeek;
                    int daysToSubtract = currentDayOfWeek == 0 ? 6 : currentDayOfWeek - 1; // Pazartesi = 1, Pazar = 0
                    startDate = DateTime.SpecifyKind(DateTime.UtcNow.Date.AddDays(-daysToSubtract), DateTimeKind.Utc);
                    endDate = DateTime.SpecifyKind(startDate.AddDays(7).AddTicks(-1), DateTimeKind.Utc);
                    break;
                case TimePeriodType.Monthly:
                    startDate = DateTime.SpecifyKind(new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 1), DateTimeKind.Utc);
                    endDate = DateTime.SpecifyKind(startDate.AddMonths(1).AddTicks(-1), DateTimeKind.Utc);
                    break;
                case TimePeriodType.Yearly:
                    startDate = DateTime.SpecifyKind(new DateTime(DateTime.UtcNow.Year, 1, 1), DateTimeKind.Utc);
                    endDate = DateTime.SpecifyKind(startDate.AddYears(1).AddTicks(-1), DateTimeKind.Utc);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(timePeriod), timePeriod, null);
            }

            return (startDate, endDate);
        }
    }
}
