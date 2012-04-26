namespace Tests.Unit.Lender.Slos.Nullability
{
    using System;

    using global::Lender.Slos.Nullability;

    using Moq;

    public static class WithoutNullabilityTestsHelper
    {
        public static Guid NextId { get; set; }

        public static WithoutNullabilityApplication CreateApplication(Guid? id = null)
        {
            var stubApplicationRepo = 
                new Mock<IRepository<ApplicationEntity>>(MockBehavior.Loose);
            stubApplicationRepo
                .Setup(e => e.Create(It.IsAny<ApplicationEntity>()))
                .Callback(() => NextId = Guid.NewGuid())
                .Returns(NextId);

            var application = 
                new WithoutNullabilityApplication(stubApplicationRepo.Object, id ?? Guid.Empty)
                    {
                        LastName = "Public", 
                        FirstName = "John", 
                        DateOfBirth = new DateTime(1991, 5, 7),
                    };
            
            return application;
        }
    }
}