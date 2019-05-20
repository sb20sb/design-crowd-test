using System;

namespace DesignCrowd.PublicHolidays.Rules
{
    public interface IPublicHolidayRule
    {
        DateTime GetPublicHoliday(int year);
    }
}
