using DesignCrowd.PublicHolidays;
using DesignCrowd.PublicHolidays.Rules;
using System;
using System.Collections.Generic;
using Xunit;

namespace DesignCrowd.Tests
{
    public class BusinessDayCounterTests
    {
        BusinessDayCounter _businessDayCounter = new BusinessDayCounter();

        [Theory]
        [MemberData(nameof(TestData_WeekdaysBetweenTwoDates))]
        public void WeekdaysBetweenTwoDates_GivesExpectedResult(DateTime firstDate, DateTime secondDate, int expectedResult)
        {
            var weekdays = _businessDayCounter.WeekdaysBetweenTwoDates(firstDate, secondDate);
            Assert.Equal(weekdays, expectedResult);
        }

        [Theory]
        [MemberData(nameof(TestData_BusinessDaysBetweenTwoDates))]
        public void BusinessDaysBetweenTwoDates_GivesExpectedResult(DateTime firstDate, DateTime secondDate, IList<DateTime> publicHolidays, int expectedResult)
        {
            var weekdays = _businessDayCounter.BusinessDaysBetweenTwoDates(firstDate, secondDate, publicHolidays);
            Assert.Equal(weekdays, expectedResult);
        }

        [Theory]
        [MemberData(nameof(TestData_BusinessDaysBetweenTwoDates_WithPublicHolidayRules))]
        public void BusinessDaysBetweenTwoDates_WithPublicHolidayRules_GivesExpectedResult(DateTime firstDate, DateTime secondDate, IList<IPublicHolidayRule> publicHolidayRules, int expectedResult)
        {
            var weekdays = _businessDayCounter.BusinessDaysBetweenTwoDates(firstDate, secondDate, publicHolidayRules);
            Assert.Equal(weekdays, expectedResult);
        }

        public static IEnumerable<object[]> TestData_WeekdaysBetweenTwoDates()
        {
            return new List<object[]>
            {
                new object[] {
                    new DateTime(2013, 10, 7), new DateTime(2013, 10, 9), 1
                },
                    new object[] {
                    new DateTime(2013, 10, 5), new DateTime(2013, 10, 14), 5
                },
                new object[] {
                    new DateTime(2013, 10, 7), new DateTime(2014, 1, 1), 61
                },
                new object[] {
                    new DateTime(2013, 10, 7), new DateTime(2013, 10, 5), 0
                },
            };
        }

        public static IEnumerable<object[]> TestData_BusinessDaysBetweenTwoDates()
        {
            var publicHolidays = new[]
            {
                new DateTime(2013, 12, 25),
                new DateTime(2013, 12, 26),
                new DateTime(2014, 1, 1),
            };

            return new List<object[]>
            {
                new object[] {
                    new DateTime(2013, 10, 7), new DateTime(2013, 10, 9), publicHolidays, 1
                },
                    new object[] {
                    new DateTime(2013, 12, 24), new DateTime(2013, 12, 27), publicHolidays, 0
                },
                new object[] {
                    new DateTime(2013, 10, 7), new DateTime(2014, 1, 1), publicHolidays, 59
                },
            };
        }

        public static IEnumerable<object[]> TestData_BusinessDaysBetweenTwoDates_WithPublicHolidayRules()
        {
            var publicHolidayRules = new IPublicHolidayRule[]
            {
                new DayOfMonthPublicHoliday(6, DayOfWeek.Monday, 2),
                new FixedDayPublicHoliday(25, 4),
                new WeekendShiftedPublicHoliday(1, 1, WeekendShiftedPublicHoliday.WeekendShiftType.After, DayOfWeek.Monday),
                new WeekendShiftedPublicHoliday(26, 1, WeekendShiftedPublicHoliday.WeekendShiftType.After, DayOfWeek.Monday),
                new WeekendShiftedPublicHoliday(25, 12, WeekendShiftedPublicHoliday.WeekendShiftType.Before, DayOfWeek.Friday),
                new WeekendShiftedPublicHoliday(26, 12, WeekendShiftedPublicHoliday.WeekendShiftType.After, DayOfWeek.Monday),
            };

            return new List<object[]>
            {
                //1 Jan 2017 was Sunday. so shifted to Monday. 
                    new object[] {
                    new DateTime(2016, 12, 31), new DateTime(2017, 1, 03), publicHolidayRules, 0
                },
                //same test dates as previous
                new object[] {
                    new DateTime(2013, 10, 7), new DateTime(2013, 10, 9), publicHolidayRules, 1
                },
                    new object[] {
                    new DateTime(2013, 12, 24), new DateTime(2013, 12, 27), publicHolidayRules, 0
                },
                new object[] {
                    new DateTime(2013, 10, 7), new DateTime(2014, 1, 1), publicHolidayRules, 59
                },
            };
        }
    }
}
