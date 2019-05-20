using System;

namespace DesignCrowd.PublicHolidays.Rules
{
    public class DayOfMonthPublicHoliday : IPublicHolidayRule
    {
        int _month;
        DayOfWeek _dayOfWeek;
        int _week;

        public DayOfMonthPublicHoliday(int month, DayOfWeek dayOfWeek, int week)
        {
            _month = month;
            _dayOfWeek = dayOfWeek;
            _week = week;
        }

        public DateTime GetPublicHoliday(int year)
        {
            var baseDate = new DateTime(year, _month, 1);

            //get first monday
            while (baseDate.DayOfWeek != _dayOfWeek)
            {
                baseDate = baseDate.AddDays(1);
            }

            return baseDate.AddDays((_week - 1) * 7);
        }
    }
}
