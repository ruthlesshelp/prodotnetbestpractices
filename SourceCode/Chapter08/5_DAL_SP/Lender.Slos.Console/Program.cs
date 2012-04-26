namespace Lender.Slos.ConsoleApp
{
    using System;

    using Lender.Slos.Model;

    public class Program
    {
        public static void Main(string[] args)
        {
            var initialForegroundColor = Console.ForegroundColor;

            try
            {
                var connectionString = ConnectionFactory.GetConnectionString();

                RepositoryFactory.ConnectionString = connectionString;

                var newApplication = new Application
                    {
                        Student =
                            {
                                LastName = "Public",
                                FirstName = "John",
                                MiddleInitial = "Question",
                                Suffix = "Sr.",
                                DateOfBirth = new DateTime(DateTime.Today.Year - 18, 11, 13),
                                HighSchool =
                                    {
                                        Name = "My High School", 
                                        City = "Anytown", 
                                        State = "QQ",
                                    },
                            },
                        Principal = 1000,
                        AnnualPercentageRate = 1.23m,
                        TotalPayments = 360,
                    };

                newApplication.Save();
                var newId = newApplication.Id;

                newApplication.Write();

                Console.WriteLine("Saved successfully");
                Console.WriteLine();

                var foundApplication = Application.FindById(newId);
                foundApplication.Write();

                Console.WriteLine("Found successfully");
                Console.WriteLine();

                foundApplication.Principal = 1001;
                foundApplication.Save();
                foundApplication.Write();

                Console.WriteLine("Changed and saved successfully");
                Console.WriteLine();

                var studentId = foundApplication.Student.Id;

                foundApplication.Remove();

                var student = Student.FindById(studentId);
                student.Remove(true);

                Console.WriteLine("Application, Student and Individual deleted successfully");
                Console.WriteLine();
            }
            catch (Exception exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Exception: {0}", exception.Message);
                Console.WriteLine("{0}", exception);
            }
            finally
            {
                Console.ForegroundColor = initialForegroundColor;
            }

            Console.WriteLine();
            Console.WriteLine("Press any key to continue . . .");
            Console.ReadKey(true);
        }
    }
}
