using System;
using System.Collections.Generic;

namespace BusinessDayCounterApp.Service.Rules
{
    public class DateRule : PublicHolidayRule
    {
        public int Month { get; set; }
        public int Day { get; set; }

        public DateRule(int month, int day)
        {
            Month = month;
            Day = day;
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
                var holiday = new DateTime(year, Month, Day);
                if (holiday > firstDate && holiday < secondDate)
                {
                    if (holiday.DayOfWeek.Equals(DayOfWeek.Saturday))
                    {
                        var substituteHolidayDate = holiday.AddDays(2);
                        if (substituteHolidayDate < secondDate)
                            occurrence++;
                    }
                    else if (holiday.DayOfWeek.Equals(DayOfWeek.Sunday))
                    {
                        var substituteHolidayDate = holiday.AddDays(1);
                        if (substituteHolidayDate < secondDate)
                            occurrence++;
                    }
                    else
                    {
                        occurrence++;
                    }
                }
            }

            return occurrence;
        }
    }
}