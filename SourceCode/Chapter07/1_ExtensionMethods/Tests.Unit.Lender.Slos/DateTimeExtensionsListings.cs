namespace Tests.Unit.Lender.Slos.Extensions
{
    using System;

    using NUnit.Framework;

    using global::Lender.Slos.Extensions;

    public class DateTimeExtensionsListings
    {
        [Test]
        public void Listing_7_1()
        {
            DateTime importantDate = new DateTime(2011, 5, 7);

            switch (importantDate.DayOfWeek)
            {
                case DayOfWeek.Saturday:
                case DayOfWeek.Sunday:
                    WeekendProcessing();
                    break;
                default:
                    WeekdayProcessing();
                    break;
            }
        }

        [Test]
        public void Listing_7_3()
        {
            DateTime importantDate = new DateTime(2011, 5, 7);

            if (DateTimeHelper.IsWeekend(importantDate))
            {
                    WeekendProcessing();
            }
            else
            {
                    WeekdayProcessing();
            }
        }

        [Test]
        public void Listing_7_5()
        {
            DateTime importantDate = new DateTime(2011, 5, 7);

            if (importantDate.IsWeekend())
            {
                WeekendProcessing();
            }
            else
            {
                WeekdayProcessing();
            }
        }

        private void WeekdayProcessing()
        {
            Console.WriteLine("WeekdayProcessing");
        }

        private void WeekendProcessing()
        {
            Console.WriteLine("WeekendProcessing");
        }
    }
}