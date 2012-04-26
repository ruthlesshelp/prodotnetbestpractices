namespace Lender.Slos.Extensions
{
    using System;

    public class HolidayInfo
    {
        public HolidayInfo(string name, DateTime observed)
        {
            Name = name;
            Observed = observed;
        }

        public string Name { get; private set; }

        public DateTime Observed { get; private set; }
    }
}