namespace Tests.Unit.Lender.Slos.Nullability
{
    using System;

    using global::Lender.Slos.Nullability;

    using Moq;

    public static class NullabilityTestsHelper
    {
        public static Guid NextId { get; set; }

        public static Application CreateApplication(Guid? id = null)
        {
            var stubApplicationRepo = 
                new Mock<IRepository<ApplicationEntity>>(MockBehavior.Loose);
            stubApplicationRepo
                .Setup(e => e.Create(It.IsAny<ApplicationEntity>()))
                .Callback(() => NextId = Guid.NewGuid())
                .Returns(NextId);

            var application = 
                new Application(stubApplicationRepo.Object, id)
                    {
                        LastName = "Public", 
                        FirstName = "John", 
                        DateOfBirth = new DateTime(1991, 5, 7),
                    };
            
            return application;
        }
    }
}