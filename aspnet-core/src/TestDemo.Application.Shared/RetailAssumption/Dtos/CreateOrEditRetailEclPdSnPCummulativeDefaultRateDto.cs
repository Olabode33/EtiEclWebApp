
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.RetailAssumption.Dtos
{
    public class CreateOrEditRetailEclPdSnPCummulativeDefaultRateDto : EntityDto<Guid?>
    {

		public string Key { get; set; }
		
		
		public string Rating { get; set; }
		
		
		public int? Years { get; set; }
		
		
		public double? Value { get; set; }
		
		
		[Required]
		public bool RequiresGroupApproval { get; set; }
		
		
		 public Guid? RetailEclId { get; set; }
		 
		 
    }
}