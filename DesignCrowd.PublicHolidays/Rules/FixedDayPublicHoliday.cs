using System;

namespace DesignCrowd.PublicHolidays.Rules
{
    public class FixedDayPublicHoliday : IPublicHolidayRule
    {
        int _day;
        int _month;

        public FixedDayPublicHoliday(int day, int month)
        {
            _day = day;
            _month = month;
        }

        public DateTime GetPublicHoliday(int year)
        {
            return new DateTime(year, _month, _day);
        }
    }
}
