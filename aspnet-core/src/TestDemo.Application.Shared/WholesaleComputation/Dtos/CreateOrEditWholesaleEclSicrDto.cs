using TestDemo.EclShared;

using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.WholesaleComputation.Dtos
{
    public class CreateOrEditWholesaleEclSicrDto : EntityDto<Guid?>
    {

		[Required]
		public int ComputedSICR { get; set; }
		
		
		public string OverrideSICR { get; set; }
		
		
		public string OverrideComment { get; set; }
		
		
		public GeneralStatusEnum Status { get; set; }
		
		
		 public Guid WholesaleEclDataLoanBookId { get; set; }
		 
		 
    }
}