
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.LoanImpairmentRecoveries.Dtos
{
    public class CreateOrEditLoanImpairmentRecoveryDto : EntityDto<Guid?>
    {

		public string Recovery { get; set; }
		
		
		public double CashRecovery { get; set; }
		
		
		public double Property { get; set; }
		
		
		public double Shares { get; set; }
		
		
		public double LoanSale { get; set; }
		
		
		public Guid RegisterId { get; set; }
		
		

    }
}