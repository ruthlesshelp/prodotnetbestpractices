namespace Lender.Slos.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class DateTimeExtensions
    {
        static DateTimeExtensions()
        {
            var year = DateTime.Today.Year;

            MinValid = new DateTime(year, 1, 1);
            MaxValid = new DateTime(year, 12, 31);
            
            Holidays = new List<HolidayInfo>();
        }

        public static DateTime MinValid { get; set; }

        public static DateTime MaxValid { get; set; }

        public static ICollection<HolidayInfo> Holidays { get; private set; }

        public static bool IsWeekend(this DateTime dateTime)
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

        public static bool IsHoliday(this DateTime dateTime)
        {
            if (dateTime.Date < MinValid.Date)
            {
                throw new ArgumentOutOfRangeException(
                    "dateTime", 
                    dateTime, 
                    string.Format("Date must not be before '{0}'", MinValid.ToShortDateString()));
            }

            if (dateTime.Date > MaxValid.Date)
            {
                throw new ArgumentOutOfRangeException(
                    "dateTime",
                    dateTime,
                    string.Format("Date must not be after '{0}'", MaxValid.ToShortDateString()));
            }

            return Holidays.Where(e => e.Observed == dateTime.Date).Any();
        }

        public static bool IsWorkingDay(this DateTime dateTime)
        {
            if (IsHoliday(dateTime)) return false;

            return !IsWeekend(dateTime);
        }
    }
}