
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.LoanImpairmentInputParameters.Dtos
{
    public class CreateOrEditLoanImpairmentInputParameterDto : EntityDto<Guid?>
    {

		public DateTime ReportingDate { get; set; }
		
		
		public double CostOfCapital { get; set; }
		
		
		public double LoanAmount { get; set; }
		
		
		public Guid RegisterId { get; set; }
		
		

    }
}