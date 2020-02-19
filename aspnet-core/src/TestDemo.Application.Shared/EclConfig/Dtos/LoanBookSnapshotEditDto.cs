namespace TestDemo.EclConfig.Dtos
{
    public class LoanBookSnapshotEditDto
    {
		public bool WholesaleStaging { get; set; }
		public bool RetailStaging { get; set; }
        public bool ObeStaging { get; set; }
    }

    public class PaymentScheduleEditDto: LoanBookSnapshotEditDto
    {}
}