namespace Tests.Unit.Lender.Slos.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using global::Lender.Slos.Extensions;

    using NUnit.Framework;

    public class DateTimeExtensionsTests
    {
        [TearDown]
        public void TestTearDown()
        {
            DateTimeExtensions.Holidays.Clear();
        }

        [TestCase(2011, 7, 4, DayOfWeek.Monday)]
        [TestCase(2011, 7, 5, DayOfWeek.Tuesday)]
        [TestCase(2011, 7, 6, DayOfWeek.Wednesday)]
        [TestCase(2011, 7, 7, DayOfWeek.Thursday)]
        [TestCase(2011, 7, 8, DayOfWeek.Friday)]
        public void IsWeekend_WithWeekDayDate_ExpectFalse(
            int year,
            int month,
            int day,
            DayOfWeek expectedDayOfWeek)
        {
            // Arrange
            DateTimeExtensions.MinValid = new DateTime(year, 1, 1);
            DateTimeExtensions.MaxValid = new DateTime(year, 12, 31);

            var classUnderTest = new DateTime(year, month, day, 23, 53, 59);

            Assert.AreEqual(expectedDayOfWeek, classUnderTest.DayOfWeek);

            // Act
            var actual = classUnderTest.IsWeekend();

            // Assert
            Assert.AreEqual(false, actual);
        }

        [TestCase(2011, 3, 5, DayOfWeek.Saturday)]
        [TestCase(2011, 10, 23, DayOfWeek.Sunday)]
        public void IsWeekend_WithWeekendDayDate_ExpectTrue(
            int year,
            int month,
            int day,
            DayOfWeek expectedDayOfWeek)
        {
            // Arrange
            DateTimeExtensions.MinValid = new DateTime(year, 1, 1);
            DateTimeExtensions.MaxValid = new DateTime(year, 12, 31);

            var classUnderTest = new DateTime(year, month, day, 23, 53, 59);

            Assert.AreEqual(expectedDayOfWeek, classUnderTest.DayOfWeek);

            // Act
            var actual = classUnderTest.IsWeekend();

            // Assert
            Assert.AreEqual(true, actual);
        }

        [TestCase(2011, 1, 1, "New Year's Day")]
        [TestCase(2011, 7, 4, "Independence Day")]
        [TestCase(2011, 12, 26, "Christmas Day")]
        public void IsHoliday_WithHolidayDate_ExpectTrue(
            int year,
            int month,
            int day,
            string expectedHolidayName)
        {
            // Arrange
            DateTimeExtensions.MinValid = new DateTime(year, 1, 1);
            DateTimeExtensions.MaxValid = new DateTime(year, 12, 31);

            AddHolidays(DateTimeExtensions.Holidays);

            var classUnderTest = new DateTime(year, month, day, 23, 53, 59);

            var holiday = DateTimeExtensions.Holidays
                .Where(e => e.Name == expectedHolidayName)
                .Single();

            Assert.AreEqual(holiday.Observed, classUnderTest.Date);
            
            // Act
            var actual = classUnderTest.IsHoliday();

            // Assert
            Assert.AreEqual(true, actual);
        }

        [TestCase(2011, 1, 16)]
        [TestCase(2011, 1, 18)]
        [TestCase(2011, 7, 29)]
        public void IsHoliday_WithNonHolidayDate_ExpectFalse(
            int year,
            int month,
            int day)
        {
            // Arrange
            DateTimeExtensions.MinValid = new DateTime(year, 1, 1);
            DateTimeExtensions.MaxValid = new DateTime(year, 12, 31);

            AddHolidays(DateTimeExtensions.Holidays);

            var classUnderTest = new DateTime(year, month, day, 23, 53, 59);

            var holiday = DateTimeExtensions.Holidays
                .Where(e => e.Observed == classUnderTest.Date)
                .FirstOrDefault();

            Assert.IsNull(holiday);

            // Act
            var actual = classUnderTest.IsHoliday();

            // Assert
            Assert.AreEqual(false, actual);
        }

        [TestCase(2011, 1, 15)]
        [TestCase(2011, 1, 16)]
        [TestCase(2011, 1, 17)]
        public void IsWorkingDay_WithNonWorkingDayDate_ExpectFalse(
            int year,
            int month,
            int day)
        {
            // Arrange
            DateTimeExtensions.MinValid = new DateTime(year, 1, 1);
            DateTimeExtensions.MaxValid = new DateTime(year, 12, 31);

            AddHolidays(DateTimeExtensions.Holidays);

            var classUnderTest = new DateTime(year, month, day, 23, 53, 59);

            // Act
            var actual = classUnderTest.IsWorkingDay();

            // Assert
            Assert.AreEqual(false, actual);
        }

        [TestCase(2011, 1, 12)]
        [TestCase(2011, 1, 13)]
        [TestCase(2011, 1, 14)]
        [TestCase(2011, 1, 18)]
        [TestCase(2011, 1, 19)]
        public void IsWorkingDay_WithWorkingDayDate_ExpectTrue(
            int year,
            int month,
            int day)
        {
            // Arrange
            DateTimeExtensions.MinValid = new DateTime(year, 1, 1);
            DateTimeExtensions.MaxValid = new DateTime(year, 12, 31);

            AddHolidays(DateTimeExtensions.Holidays);

            var classUnderTest = new DateTime(year, month, day, 23, 53, 59);

            // Act
            var actual = classUnderTest.IsWorkingDay();

            // Assert
            Assert.AreEqual(true, actual);
        }

        [TestCase(2010, 12, 31, "Date must not be before '1/1/2011'\r\nParameter name: dateTime\r\nActual value was 12/31/2010 11:53:59 PM.")]
        public void IsHoliday_WithDateBeforeMinValid_ExpectArgumentOutOfRangeException(
            int year,
            int month,
            int day,
            string expectedMessage)
        {
            // Arrange
            DateTimeExtensions.MinValid = new DateTime(2011, 1, 1);
            DateTimeExtensions.MaxValid = new DateTime(2011, 12, 31);

            var classUnderTest = new DateTime(year, month, day, 23, 53, 59);

            // Act
            TestDelegate act = () => classUnderTest.IsHoliday();

            // Assert
            var result = Assert.Throws<ArgumentOutOfRangeException>(act);
            Assert.AreEqual(expectedMessage, result.Message);
        }

        [TestCase(2011, 1, 1, "Date must not be after '12/31/2010'\r\nParameter name: dateTime\r\nActual value was 1/1/2011 11:53:59 PM.")]
        public void IsHoliday_WithDateAfterMaxValid_ExpectArgumentOutOfRangeException(
            int year,
            int month,
            int day,
            string expectedMessage)
        {
            // Arrange
            DateTimeExtensions.MinValid = new DateTime(2010, 1, 1);
            DateTimeExtensions.MaxValid = new DateTime(2010, 12, 31);

            var classUnderTest = new DateTime(year, month, day, 23, 53, 59);

            // Act
            TestDelegate act = () => classUnderTest.IsHoliday();

            // Assert
            var result = Assert.Throws<ArgumentOutOfRangeException>(act);
            Assert.AreEqual(expectedMessage, result.Message);
        }

        private static void AddHolidays(ICollection<HolidayInfo> holidays)
        {
            if (holidays.Count > 0) holidays.Clear();

            // U.S. Bank Holidays 2011
            holidays.Add(new HolidayInfo("New Year's Day", new DateTime(2011, 1, 1)));
            holidays.Add(new HolidayInfo("Martin L King's Birthday", new DateTime(2011, 1, 17)));
            holidays.Add(new HolidayInfo("Washington's Birthday", new DateTime(2011, 2, 21)));
            holidays.Add(new HolidayInfo("Memorial Day", new DateTime(2011, 5, 30)));
            holidays.Add(new HolidayInfo("Independence Day", new DateTime(2011, 7, 4)));
            holidays.Add(new HolidayInfo("Labor Day", new DateTime(2011, 9, 5)));
            holidays.Add(new HolidayInfo("Columbus Day", new DateTime(2011, 10, 10)));
            holidays.Add(new HolidayInfo("Veteran's Day", new DateTime(2011, 11, 11)));
            holidays.Add(new HolidayInfo("Thanksgiving Day", new DateTime(2011, 11, 24)));
            holidays.Add(new HolidayInfo("Christmas Day", new DateTime(2011, 12, 26)));
        }
    }
}
