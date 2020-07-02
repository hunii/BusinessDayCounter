namespace BusinessDayCounterApp.Service.Factory
{
    public interface IDayCounterFactory
    {
        BusinessDayCounter CreateBusinessDayCounter();
        EnhancedBusinessDayCounter CreateEnhancedBusinessDayCounter();
    }
}