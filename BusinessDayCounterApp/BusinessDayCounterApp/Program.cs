using BusinessDayCounterApp.Service.Factory;
using BusinessDayCounterApp.Service.Rules;
using BusinessDayCounterApp.Service.Types;
using System;
using System.Collections.Generic;

namespace BusinessDayCounterApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var counterFactory = new DayCounterFactory();

            Console.WriteLine("Task 1");
            TestWeekdaysBetweenTwoDates(counterFactory, new DateTime(2013, 10, 7), new DateTime(2013, 10, 9));
            TestWeekdaysBetweenTwoDates(counterFactory, new DateTime(2013, 10, 5), new DateTime(2013, 10, 14));
            TestWeekdaysBetweenTwoDates(counterFactory, new DateTime(2013, 10, 7), new DateTime(2014, 1, 1));
            TestWeekdaysBetweenTwoDates(counterFactory, new DateTime(2013, 10, 7), new DateTime(2013, 10, 5));

            Console.WriteLine("Task 2");
            var holidays = new List<DateTime>
            {
                new DateTime(2013, 12, 25),
                new DateTime(2013, 12, 26),
                new DateTime(2014, 1, 1),
            };
            TestBusinessdaysBetweenTwoDates(counterFactory, new DateTime(2013, 10, 7), new DateTime(2013, 10, 9), holidays);
            TestBusinessdaysBetweenTwoDates(counterFactory, new DateTime(2013, 12, 24), new DateTime(2013, 12, 27), holidays);
            TestBusinessdaysBetweenTwoDates(counterFactory, new DateTime(2013, 10, 7), new DateTime(2014,1,1), holidays);

            Console.WriteLine("Task 3");
            var rules = new List<PublicHolidayRule> 
            { 
                new DateRule(4, 25), //ANZAC Day
                new DateRule(12, 25), //Christmas
                new DateRule(12, 26), //Day after Christmas
                new DateRule(1, 1), //New Year
                new DayOfWeekInMonthInWeekRule(6, WeekTypes.Week2, DayOfWeek.Monday) //Queens Birthday on the second Monday in June every year
            };
            TestBusinessdaysBetweenTwoDatesWithComplexHolidayRules(counterFactory, new DateTime(2013, 10, 7), new DateTime(2013, 10, 9), rules);
            TestBusinessdaysBetweenTwoDatesWithComplexHolidayRules(counterFactory, new DateTime(2013, 12, 24), new DateTime(2013, 12, 27), rules);
            TestBusinessdaysBetweenTwoDatesWithComplexHolidayRules(counterFactory, new DateTime(2013, 10, 7), new DateTime(2014, 1, 1), rules);
            TestBusinessdaysBetweenTwoDatesWithComplexHolidayRules(counterFactory, new DateTime(2013, 6, 8), new DateTime(2013, 6, 12), rules);

            Console.ReadLine();
        }

        static void TestWeekdaysBetweenTwoDates(IDayCounterFactory factory, DateTime firstDate, DateTime secondDate)
        {
            var weekdays = factory.CreateBusinessDayCounter().WeekdaysBetweenTwoDates(firstDate, secondDate);
            Console.WriteLine($"{firstDate.ToString("yyyy-MM-dd")} to {secondDate.ToString("yyyy-MM-dd")} = {weekdays} week days");
        }

        static void TestBusinessdaysBetweenTwoDates(IDayCounterFactory factory, DateTime firstDate, DateTime secondDate, IList<DateTime> holidays)
        {
            var businessDays = factory.CreateBusinessDayCounter().BusinessDaysBetweenTwoDates(firstDate, secondDate, holidays);
            Console.WriteLine($"{firstDate.ToString("yyyy-MM-dd")} to {secondDate.ToString("yyyy-MM-dd")} = {businessDays} business days");
        }

        static void TestBusinessdaysBetweenTwoDatesWithComplexHolidayRules(IDayCounterFactory factory, DateTime firstDate, DateTime secondDate, IList<PublicHolidayRule> rules)
        {
            var businessDays = factory.CreateEnhancedBusinessDayCounter().BusinessDaysBetweenTwoDates(firstDate, secondDate, rules);
            Console.WriteLine($"{firstDate.ToString("yyyy-MM-dd")} to {secondDate.ToString("yyyy-MM-dd")} = {businessDays} business days");
        }
    }
}
