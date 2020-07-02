using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessDayCounterApp.Service.Extensions
{
    public static class DayOfWeekExtensions
    {
        public static bool IsWeekend(this DayOfWeek dayOfWeek)
        {
            return dayOfWeek == DayOfWeek.Saturday || dayOfWeek == DayOfWeek.Sunday;
        }
    }
}
