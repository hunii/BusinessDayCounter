using BusinessDayCounterApp.Service.Extensions;
using System;
using System.Collections.Generic;

namespace BusinessDayCounterApp.Service
{
    public class BusinessDayCounter
    {
        protected const int TOTAL_DAYS_OF_DAYS_PER_WEEK = 7;
        protected const int TOTAL_DAYS_OF_WEEKENDS_PER_WEEK = 2;

        public int WeekdaysBetweenTwoDates(DateTime firstDate, DateTime secondDate)
        {
            firstDate = firstDate.Date;
            secondDate = secondDate.Date;

            var totalDaysInTwoDates = (int)(secondDate - firstDate).TotalDays + 1;
            if (totalDaysInTwoDates <= 0)
            {
                return 0;
            }

            var daysRequiredToStartOfTheWeek = (int)firstDate.DayOfWeek - (int)DayOfWeek.Sunday;
            var daysRequiredToEndOfTheWeek = (int)DayOfWeek.Saturday - (int)secondDate.DayOfWeek;

            var totalDaysOfWeek = daysRequiredToStartOfTheWeek + totalDaysInTwoDates + daysRequiredToEndOfTheWeek;
            var totalDaysOfWeekends = (totalDaysOfWeek / TOTAL_DAYS_OF_DAYS_PER_WEEK) * TOTAL_DAYS_OF_WEEKENDS_PER_WEEK;

            var weekdaysBetweenTwoDates = totalDaysOfWeek - totalDaysOfWeekends - daysRequiredToStartOfTheWeek - daysRequiredToEndOfTheWeek;

            if (firstDate.DayOfWeek.IsWeekend())
            {
                weekdaysBetweenTwoDates++;
            }

            return weekdaysBetweenTwoDates;
        }

        public int BusinessDaysBetweenTwoDates(DateTime firstDate, DateTime secondDate, IList<DateTime> publicHolidays)
        {
            var weekdaysBetweenTwoDates = WeekdaysBetweenTwoDates(firstDate, secondDate);

            foreach(var holidayDate in publicHolidays)
            {
                if (holidayDate > firstDate && holidayDate < secondDate)
                {
                    if (!holidayDate.DayOfWeek.IsWeekend())
                        weekdaysBetweenTwoDates--;
                }
            }

            return weekdaysBetweenTwoDates;
        }
    }
}