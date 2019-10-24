
using System;
using Abp.Application.Services.Dto;

namespace TestDemo.WholesaleComputation.Dtos
{
    public class WholesaleEclComputedEadResultDto : EntityDto<Guid>
    {
		public string LifetimeEAD { get; set; }


		 public Guid? WholesaleEclDataLoanBookId { get; set; }

		 
    }
}