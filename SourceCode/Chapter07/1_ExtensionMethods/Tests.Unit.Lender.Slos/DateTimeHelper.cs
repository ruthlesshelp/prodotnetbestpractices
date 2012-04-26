namespace Tests.Unit.Lender.Slos.Extensions
{
    using System;

    public static class DateTimeHelper
    {
        public static bool IsWeekend(DateTime dateTime)
        {
            switch (dateTime.DayOfWeek)
            {
                case DayOfWeek.Saturday:
                case DayOfWeek.Sunday:
                    return true;
                default:
                    return false;
            }
        }
    }
}