using BusinessDayCounterApp.Service.Factory;
using System;
using System.Collections.Generic;
using Xunit;

namespace BusinessDayCounter.Test
{
    public class TestTask2
    {
        public static readonly object[][] TestData =
        {
            new object[] { new DateTime(2013, 10, 7), new DateTime(2013, 10, 9), 1 },
            new object[] { new DateTime(2013, 12, 24), new DateTime(2013, 12, 27), 0 },
            new object[] { new DateTime(2013, 10, 7), new DateTime(2014, 1, 1), 59 },
        };

        public static IList<DateTime> Holidays = new List<DateTime>
        {
            new DateTime(2013, 12, 25),
            new DateTime(2013, 12, 26),
            new DateTime(2014, 1, 1),
        };

        [Theory, MemberData(nameof(TestData))]
        public void GivenTwoDates_ShouldCalculateBusinessDays(DateTime firstDate, DateTime secondDate, int expected)
        {
            var factory = new DayCounterFactory();

            var businessdaysBetweenDates = factory.CreateBusinessDayCounter().BusinessDaysBetweenTwoDates(firstDate, secondDate, Holidays);

            Assert.Equal(expected, businessdaysBetweenDates);
        }
    }
}
