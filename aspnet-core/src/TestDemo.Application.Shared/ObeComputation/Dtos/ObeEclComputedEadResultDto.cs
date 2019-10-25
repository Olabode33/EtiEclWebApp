
using System;
using Abp.Application.Services.Dto;

namespace TestDemo.ObeComputation.Dtos
{
    public class ObeEclComputedEadResultDto : EntityDto<Guid>
    {
		public string LifetimeEAD { get; set; }


		 public Guid? ObeEclDataLoanBookId { get; set; }

		 
    }
}