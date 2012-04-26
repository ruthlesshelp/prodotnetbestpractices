namespace Lender.Slos.Dao
{
    public class ApplicationEntity
    {
        public virtual int Id { get; set; }

        public virtual int StudentId { get; set; }

        public virtual decimal Principal { get; set; }

        public virtual decimal AnnualPercentageRate { get; set; }

        public virtual int TotalPayments { get; set; }
    }
}
