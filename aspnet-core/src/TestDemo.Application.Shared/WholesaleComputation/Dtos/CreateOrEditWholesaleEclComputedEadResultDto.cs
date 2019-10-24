
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.WholesaleComputation.Dtos
{
    public class CreateOrEditWholesaleEclComputedEadResultDto : EntityDto<Guid?>
    {

		public string LifetimeEAD { get; set; }
		
		
		 public Guid? WholesaleEclDataLoanBookId { get; set; }
		 
		 
    }
}