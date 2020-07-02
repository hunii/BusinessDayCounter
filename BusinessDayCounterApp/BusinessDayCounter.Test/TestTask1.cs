using BusinessDayCounterApp.Service.Factory;
using System;
using Xunit;

namespace BusinessDayCounter.Test
{
    public class TestTask1
    {
        public static readonly object[][] TestData =
        {
            new object[] { new DateTime(2013, 10, 7), new DateTime(2013, 10, 9), 1 },
            new object[] { new DateTime(2013, 10, 5), new DateTime(2013, 10, 14), 5 },
            new object[] { new DateTime(2013, 10, 7), new DateTime(2014, 1, 1), 61 },
            new object[] { new DateTime(2013, 10, 7), new DateTime(2013, 10, 5), 0 }
        };

        [Theory, MemberData(nameof(TestData))]
        public void GivenTwoDates_ShouldCalculateWeekDays(DateTime firstDate, DateTime secondDate, int expected)
        {
            var factory = new DayCounterFactory();

            var weekdaysBetweenDates = factory.CreateBusinessDayCounter().WeekdaysBetweenTwoDates(firstDate, secondDate);

            Assert.Equal(expected, weekdaysBetweenDates);
        }
    }
}
