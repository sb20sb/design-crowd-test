using DesignCrowd.PublicHolidays.Extensions;
using System;

namespace DesignCrowd.PublicHolidays.Rules
{
    public class WeekendShiftedPublicHoliday : IPublicHolidayRule
    {
        int _day;
        int _month;
        WeekendShiftType _weekendShiftType;
        DayOfWeek _dayOfWeek;

        public WeekendShiftedPublicHoliday(int day, int month, WeekendShiftType weekendShiftType, DayOfWeek dayOfWeek)
        {
            _day = day;
            _month = month;
            _weekendShiftType = weekendShiftType;
            _dayOfWeek = dayOfWeek;
        }

        public DateTime GetPublicHoliday(int year)
        {
            var baseDate = new DateTime(year, _month, _day);
            if (!baseDate.IsWeekend())
                return baseDate;

            var dayToAdd = _weekendShiftType == WeekendShiftType.After ? 1 : -1;
            while (baseDate.DayOfWeek != _dayOfWeek)
            {              
                baseDate = baseDate.AddDays(dayToAdd);
            }

            return baseDate;
        }

        public enum WeekendShiftType
        {
            After,
            Before,
        }
    }
}
