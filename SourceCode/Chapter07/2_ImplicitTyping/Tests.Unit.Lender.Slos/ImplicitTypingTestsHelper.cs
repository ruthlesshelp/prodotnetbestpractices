namespace Tests.Unit.Lender.Slos.ImplicitTyping
{
    using global::Lender.Slos.ImplicitTyping;

    public static class ImplicitTypingTestsHelper
    {
        public static int NextId { get; set; }

        public static Controller CreateController(int? id = null)
        {
            var repository = new ApplicationRepository();

            return new Controller(repository);
        }
    }
}