namespace Lender.Slos.ConsoleApp
{
    using System;
    using System.Globalization;

    using Lender.Slos.Model;

    public static class OutputHelpers
    {
        public static void Write(this Application application)
        {
            Console.WriteLine("Application (Id={0})", application.Id);

            var student = application.Student;
            student.Write();

            Console.WriteLine(
                "\tPrincipal Amount: {0}", 
                application.Principal.ToString("C", new CultureInfo("EN-us")));
            Console.WriteLine(
                "\t   Interest Rate: {0}% APR", 
                application.AnnualPercentageRate);
            Console.WriteLine(
                "\t  Total Payments: {0} months", 
                application.TotalPayments);
        }

        public static void Write(this Student student)
        {
            Console.WriteLine(
                "\tStudent (Id={0}): {1}, {2}{3}{4}",
                student.Id,
                student.LastName,
                student.FirstName,
                string.IsNullOrWhiteSpace(student.MiddleInitial)
                    ? null
                    : string.Format(" {0}.", student.MiddleInitial.TrimStart()[0]),
                string.IsNullOrWhiteSpace(student.Suffix) 
                    ? null 
                    : student.Suffix.Trim());

            Console.WriteLine("\tDate of Birth: {0}",
                student.DateOfBirth.ToLongDateString());

            var highSchool = student.HighSchool;
            highSchool.Write();
        }

        public static void Write(this School school)
        {
            Console.WriteLine("\tHighSchool: {0}, {1}, {2}", 
                school.Name, 
                school.City, 
                school.State);
        }
    }
}
