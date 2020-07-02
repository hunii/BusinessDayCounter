using BusinessDayCounterApp.Service.Types;
using System;
using System.Collections.Generic;

namespace BusinessDayCounterApp.Service.Rules
{
    public class DayOfWeekInMonthInWeekRule : PublicHolidayRule
    {
        public int Month { get; set; }
        public WeekTypes Week { get; set; }
        public DayOfWeek DayOfWeek { get; set; }

        public DayOfWeekInMonthInWeekRule(int month, WeekTypes week, DayOfWeek dayOfWeek)
        {
            Month = month;
            Week = week;
            DayOfWeek = dayOfWeek;
        }

        public override int DaysOfOccurrence(DateTime firstDate, DateTime secondDate)
        {
            firstDate = firstDate.Date;
            secondDate = secondDate.Date;

            if (secondDate <= firstDate)
                return 0;

            var occurrence = 0;
            var years = new List<int> { firstDate.Year };

            if (!firstDate.Year.Equals(secondDate.Year))
                years.Add(secondDate.Year);

            foreach (var year in years)
            {
                var holiday = GetHolidayDate(year);
                if (holiday > firstDate && holiday < secondDate)
                {
                    occurrence++;
                }
            }

            return occurrence;
        }

        private DateTime GetHolidayDate(int year)
        {
            var firstDateOfMonthYear = new DateTime(year, Month, 1);

            var firstOccurrenceHolidayDate = firstDateOfMonthYear.DayOfWeek == DayOfWeek 
                                        ? firstDateOfMonthYear 
                                        : firstDateOfMonthYear.AddDays(7 + DayOfWeek - firstDateOfMonthYear.DayOfWeek);

            var daysToAdd = ((int)Week - 1) * 7;

            return firstOccurrenceHolidayDate.AddDays(daysToAdd);
        }
    }
}


