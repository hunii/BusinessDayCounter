using System;

namespace BusinessDayCounterApp.Service.Rules
{
    public abstract class PublicHolidayRule
    {
        public abstract int DaysOfOccurrence(DateTime firstDate, DateTime secondDate);
    }
}