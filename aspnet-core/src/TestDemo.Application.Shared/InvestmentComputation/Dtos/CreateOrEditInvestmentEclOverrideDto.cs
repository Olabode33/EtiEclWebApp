using TestDemo.EclShared;

using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.InvestmentComputation.Dtos
{
    public class CreateOrEditInvestmentEclOverrideDto : EntityDto<Guid?>
    {

		public int StageOverride { get; set; }
		
		
		[Required]
		public string OverrideComment { get; set; }
		
		
		public GeneralStatusEnum Status { get; set; }
		
		
		 public Guid InvestmentEclSicrId { get; set; }
		 
		 
    }
}