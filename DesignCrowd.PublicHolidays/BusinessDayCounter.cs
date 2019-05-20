using System;
using System.Collections.Generic;
using System.Linq;
using DesignCrowd.PublicHolidays.Extensions;
using DesignCrowd.PublicHolidays.Rules;

namespace DesignCrowd.PublicHolidays
{
    public class BusinessDayCounter
    {
        public int WeekdaysBetweenTwoDates(DateTime firstDate, DateTime secondDate)
        {
            if (secondDate.Date <= firstDate.Date)
                return 0;

            var days = (secondDate.Date - firstDate.Date).Days - 1;

            var weeks = days / 7;

            var workDaysInWeeks = weeks * 5;

            var daysLeft = days - weeks * 7; //or days % 7
            for (int i = 0; i < daysLeft; i++)
            {
                if (!firstDate.Date.AddDays(i + 1).IsWeekend())
                {
                    workDaysInWeeks++;
                }
            }

            return workDaysInWeeks;
        }

        public int BusinessDaysBetweenTwoDates(DateTime firstDate, DateTime secondDate, IList<DateTime> publicHolidays)
        {
            var weekdays = WeekdaysBetweenTwoDates(firstDate, secondDate);
            foreach (var holiday in publicHolidays)
            {
                if (holiday.Date > firstDate.Date && holiday.Date < secondDate)
                {
                    weekdays--;
                }
            }
            return weekdays;
        }

        public int BusinessDaysBetweenTwoDates(DateTime firstDate, DateTime secondDate, IList<IPublicHolidayRule> publicHolidayRules)
        {
            var publicHolidays = new List<DateTime>();

            for (int year = firstDate.Year; year <= secondDate.Year; year++)
            {
                publicHolidays.AddRange(publicHolidayRules.Select(h => h.GetPublicHoliday(year)));
            }

            return BusinessDaysBetweenTwoDates(firstDate, secondDate, publicHolidays);
        }
    }
}
