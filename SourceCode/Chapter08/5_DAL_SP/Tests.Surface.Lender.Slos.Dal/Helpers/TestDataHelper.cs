namespace Tests.Surface.Lender.Slos.Dal.Helpers
{
    using System;
    using System.Data.SqlTypes;
    using System.Text;

    using NUnit.Framework;

    internal static class TestDataHelper
    {
        public const int DefaultMaxStringLength = 50;
        public const string UpperCaseLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public const string LowerCaseLetters = "abcdefghijklmnopqrstuvwxyz";

        public const decimal DefaultMinimumMoneyAmount = 1000.00m;
        public const decimal DefaultMaximumMoneyAmount = 1000000.00m;

        public const decimal DefaultMinimumPercentageRate = 1.0000m;
        public const decimal DefaultMaximumPercentageRate = 20.0000m;

        public const int DefaultMinimumCount = 1;
        public const int DefaultMaximumCount = 1000;

        public static string BuildNameString(
            int? length = null)
        {
            var randomizer = new Random();
            var generatedLength = length ?? randomizer.Next(1, DefaultMaxStringLength);

            Assert.Greater(generatedLength, 0);

            var stringBuilder = new StringBuilder(generatedLength);
            stringBuilder.Append(UpperCaseLetters[randomizer.Next(0, UpperCaseLetters.Length - 1)]);
            for (var index = 1; index < generatedLength; index++)
            {
                stringBuilder.Append(LowerCaseLetters[randomizer.Next(0, LowerCaseLetters.Length - 1)]);
            }

            return stringBuilder.ToString();
        }

        public static SqlDateTime BuildSqlDateTime(
            DateTime? minValue = null,
            DateTime? maxValue = null)
        {
            var randomizer = new Random();

            // SqlDateTime must be between 1/1/1753 12:00:00 AM and 12/31/9999 11:59:59 PM
            var minSqlDateTime = new SqlDateTime(minValue ?? SqlDateTime.MinValue.Value);
            var maxSqlDateTime = new SqlDateTime(maxValue ?? SqlDateTime.MaxValue.Value);

            Assert.Greater(maxSqlDateTime, minSqlDateTime, 
                "TestDataHelper error: maxValue must be greater than minValue");

            var range = (maxSqlDateTime.Value - minSqlDateTime.Value).Ticks;
            return new DateTime((long)(minSqlDateTime.Value.Ticks + (randomizer.NextDouble() * range)));
        }

        public static decimal BuildMoney(
            decimal? minValue = null,
            decimal? maxValue = null)
        {
            var randomizer = new Random();

            // By default money is in the range of $1,000.00 to $1,000,000.00
            var minMoney = minValue ?? DefaultMinimumMoneyAmount;
            var maxMoney = maxValue ?? DefaultMaximumMoneyAmount;

            Assert.Greater(maxMoney, minMoney,
                "TestDataHelper error: maxValue must be greater than minValue");

            var range = maxMoney - minMoney;

            return Math.Round(
                minMoney + 
                (range * (decimal)randomizer.NextDouble()), 2, MidpointRounding.AwayFromZero);
        }

        public static decimal BuildPercentageRate(
            decimal? minValue = null,
            decimal? maxValue = null)
        {
            var randomizer = new Random();

            // By default percentage rate is in the range of 1.0000% to 20.0000%
            var minRate = minValue ?? DefaultMinimumPercentageRate;
            var maxRate = maxValue ?? DefaultMaximumPercentageRate;

            Assert.Greater(maxRate, minRate,
                "TestDataHelper error: maxValue must be greater than minValue");

            var range = maxRate - minRate;

            return Math.Round(
                minRate + 
                (range * (decimal)randomizer.NextDouble()), 4, MidpointRounding.AwayFromZero);
        }

        public static int BuildCount(
            int? minValue = null,
            int? maxValue = null)
        {
            var randomizer = new Random();

            // By default total number of payments is in the range of 1 and 1000
            var minRate = minValue ?? DefaultMinimumCount;
            var maxRate = maxValue ?? DefaultMaximumCount;

            Assert.Greater(maxRate, minRate,
                "TestDataHelper error: maxValue must be greater than minValue");

            var range = maxRate - minRate;

            return minRate + randomizer.Next(0, range);
        }
    }
}
