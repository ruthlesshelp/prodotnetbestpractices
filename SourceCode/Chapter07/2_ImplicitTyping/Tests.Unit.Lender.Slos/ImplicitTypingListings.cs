namespace Tests.Unit.Lender.Slos.ImplicitTyping
{
    using System;

    using global::Lender.Slos.ImplicitTyping;

    using NUnit.Framework;

    public class ImplicitTypingListings
    {
        [Test]
        public void Search_Listing_7_6()
        {
            ApplicationRepository repository
                = new ApplicationRepository();

            string lastNameCriteria = "P";

            Controller controller = new Controller(repository);

            ApplicationCollection results = controller.Search(lastNameCriteria);

            foreach (var result in results)
            {
                Console.WriteLine("{0}, {1}",
                    result.LastName,
                    result.FirstName);
            }
        }

        [Test]
        public void Search_Listing_7_7()
        {
            var repository = new ApplicationRepository();

            var lastNameCriteria = "P";

            var controller = new Controller(repository);

            var results = controller.Search(lastNameCriteria);

            foreach (var result in results)
            {
                Console.WriteLine("{0}, {1}",
                    result.LastName,
                    result.FirstName);
            }
        }
    }
}