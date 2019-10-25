
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.ObeAssumption.Dtos
{
    public class CreateOrEditObeEclPdSnPCummulativeDefaultRateDto : EntityDto<Guid?>
    {

		public string Key { get; set; }
		
		
		public string Rating { get; set; }
		
		
		public int? Years { get; set; }
		
		
		public double? Value { get; set; }
		
		
		[Required]
		public bool RequiresGroupApproval { get; set; }
		
		
		 public Guid? ObeEclId { get; set; }
		 
		 
    }
}