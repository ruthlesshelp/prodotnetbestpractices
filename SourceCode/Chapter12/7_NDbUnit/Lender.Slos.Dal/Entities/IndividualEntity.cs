namespace Lender.Slos.Dal.Entities
{
    using System;

    public class IndividualEntity
    {
        public virtual int Id { get; set; }

        public virtual string LastName { get; set; }

        public virtual string FirstName { get; set; }

        public virtual string MiddleName { get; set; }

        public virtual string Suffix { get; set; }

        public virtual DateTime DateOfBirth { get; set; }
    }
}
