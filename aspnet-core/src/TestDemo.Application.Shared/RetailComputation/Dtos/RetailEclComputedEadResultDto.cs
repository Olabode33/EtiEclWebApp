
using System;
using Abp.Application.Services.Dto;

namespace TestDemo.RetailComputation.Dtos
{
    public class RetailEclComputedEadResultDto : EntityDto<Guid>
    {
		public string LifetimeEAD { get; set; }


		 public Guid? RetailEclDataLoanBookId { get; set; }

		 
    }
}