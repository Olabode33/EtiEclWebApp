
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.ObeComputation.Dtos
{
    public class CreateOrEditObeEclComputedEadResultDto : EntityDto<Guid?>
    {

		public string LifetimeEAD { get; set; }
		
		
		 public Guid? ObeEclDataLoanBookId { get; set; }
		 
		 
    }
}