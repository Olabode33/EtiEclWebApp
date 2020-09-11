
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.LoanImpairmentKeyParameters.Dtos
{
    public class InputLoanImpairmentKeyParameterDto : EntityDto<Guid?>
    {		
		
		public int Year { get; set; }
		
		
		public double ExpectedCashFlow { get; set; }
		
		
		public double RevisedCashFlow { get; set; }
		
		

    }
}