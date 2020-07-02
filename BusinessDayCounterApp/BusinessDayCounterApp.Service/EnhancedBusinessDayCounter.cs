using BusinessDayCounterApp.Service.Rules;
using System;
using System.Collections.Generic;

namespace BusinessDayCounterApp.Service
{
    public class EnhancedBusinessDayCounter : BusinessDayCounter
    {
        public int BusinessDaysBetweenTwoDates(DateTime firstDate, DateTime secondDate, IList<PublicHolidayRule> publicHolidayRules)
        {
            var weekdaysBetweenTwoDates = WeekdaysBetweenTwoDates(firstDate, secondDate);

            foreach (var rule in publicHolidayRules)
            {
                weekdaysBetweenTwoDates -= rule.DaysOfOccurrence(firstDate, secondDate);
            }

            return weekdaysBetweenTwoDates;
        }
    }
}