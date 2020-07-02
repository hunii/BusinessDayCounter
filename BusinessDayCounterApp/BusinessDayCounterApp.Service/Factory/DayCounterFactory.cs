using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessDayCounterApp.Service.Factory
{
    public class DayCounterFactory : IDayCounterFactory
    {
        public BusinessDayCounter CreateBusinessDayCounter()
        {
            return new BusinessDayCounter();
        }

        public EnhancedBusinessDayCounter CreateEnhancedBusinessDayCounter()
        {
            return new EnhancedBusinessDayCounter();
        }
    }
}