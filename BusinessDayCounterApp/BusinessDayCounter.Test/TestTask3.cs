using BusinessDayCounterApp.Service;
using BusinessDayCounterApp.Service.Factory;
using BusinessDayCounterApp.Service.Rules;
using BusinessDayCounterApp.Service.Types;
using System;
using System.Collections.Generic;
using Xunit;

namespace BusinessDayCounter.Test
{
    public class TestTask3
    {
        [Fact]
        public void GivenDateRule_WhenHolidayIsInWeekday_ShouldCalculateBusinessDays()
        {
            var rules = new List<PublicHolidayRule>
            {
                new DateRule(4, 25) //ANZAC Day
            };

            var firstDate = new DateTime(2013, 4, 24);
            var secondDate = new DateTime(2013, 4, 27);

            var factory = new DayCounterFactory();

            var businessdaysBetweenDates = factory.CreateEnhancedBusinessDayCounter().BusinessDaysBetweenTwoDates(firstDate, secondDate, rules);

            Assert.Equal(1, businessdaysBetweenDates);
        }

        [Fact]
        public void GivenDateRule_WhenHolidayIsInWeekends_ShouldHolidayMoveToMonday()
        {
            var rules = new List<PublicHolidayRule>
            {
                new DateRule(4, 25) //ANZAC Day
            };

            var firstDate = new DateTime(2020, 4, 24);
            var secondDate = new DateTime(2020, 4, 29);

            var factory = new DayCounterFactory();

            var businessdaysBetweenDates = factory.CreateEnhancedBusinessDayCounter().BusinessDaysBetweenTwoDates(firstDate, secondDate, rules);

            Assert.Equal(1, businessdaysBetweenDates);
        }

        [Fact]
        public void GivenDayOfWeekInMonthInWeekRule_ShouldCalculateBusinessDays()
        {
            var rules = new List<PublicHolidayRule>
            {
                new DayOfWeekInMonthInWeekRule(6, WeekTypes.Week2, DayOfWeek.Monday) //Queens Birthday on the second Monday in June every year
            };

            var firstDate = new DateTime(2013, 5, 31);
            var secondDate = new DateTime(2013, 6, 12);

            var factory = new DayCounterFactory();

            var businessdaysBetweenDates = factory.CreateEnhancedBusinessDayCounter().BusinessDaysBetweenTwoDates(firstDate, secondDate, rules);

            Assert.Equal(6, businessdaysBetweenDates);
        }
    }
}
