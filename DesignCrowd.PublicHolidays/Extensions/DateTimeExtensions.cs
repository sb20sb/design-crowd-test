using System;

namespace DesignCrowd.PublicHolidays.Extensions
{
    public static class DateTimeExtensions
    {
        public static bool IsWeekend(this DateTime dateTime)
        {
            return (dateTime.DayOfWeek == DayOfWeek.Sunday || dateTime.DayOfWeek == DayOfWeek.Saturday);
        }

        public static bool IsSameDay(this DateTime dateTime, DateTime comparisonDate)
        {
            return (dateTime.Day == comparisonDate.Day && dateTime.Month == comparisonDate.Month);
        }
    }
}
