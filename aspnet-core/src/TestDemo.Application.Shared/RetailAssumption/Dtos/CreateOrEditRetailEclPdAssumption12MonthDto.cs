
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.RetailAssumption.Dtos
{
    public class CreateOrEditRetailEclPdAssumption12MonthDto : EntityDto<Guid?>
    {

		[Required]
		public int Credit { get; set; }
		
		
		public double? PD { get; set; }
		
		
		public string SnPMappingEtiCreditPolicy { get; set; }
		
		
		public string SnPMappingBestFit { get; set; }
		
		
		[Required]
		public bool RequiresGroupApproval { get; set; }
		
		
		 public Guid? RetailEclId { get; set; }
		 
		 
    }
}