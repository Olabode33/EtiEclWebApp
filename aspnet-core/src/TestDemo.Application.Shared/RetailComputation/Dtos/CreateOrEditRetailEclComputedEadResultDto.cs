
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.RetailComputation.Dtos
{
    public class CreateOrEditRetailEclComputedEadResultDto : EntityDto<Guid?>
    {

		public string LifetimeEAD { get; set; }
		
		
		 public Guid? RetailEclDataLoanBookId { get; set; }
		 
		 
    }
}