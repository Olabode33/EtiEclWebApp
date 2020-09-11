using System;

namespace TestDemo.LoanImpairmentsRegisters.Dtos
{
    public class GetLoanImpairmentRegisterForViewDto
    {
		public LoanImpairmentRegisterDto LoanImpairmentRegister { get; set; }

        public DateTime DateCreated { get; set; }
        public string CreatedBy { get; set; }
    }
}