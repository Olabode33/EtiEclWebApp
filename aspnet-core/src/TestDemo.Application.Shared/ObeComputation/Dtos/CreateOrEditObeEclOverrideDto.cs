
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.ObeComputation.Dtos
{
    public class CreateOrEditObeEclOverrideDto : EntityDto<Guid?>
    {

		public double? Stage { get; set; }
		
		
		public double? TtrYears { get; set; }
		
		
		public double? FSV_Cash { get; set; }
		
		
		public double? FSV_CommercialProperty { get; set; }
		
		
		public double? FSV_Debenture { get; set; }
		
		
		public double? FSV_Inventory { get; set; }
		
		
		public double? FSV_PlantAndEquipment { get; set; }
		
		
		public double? FSV_Receivables { get; set; }
		
		
		public double? FSV_ResidentialProperty { get; set; }
		
		
		public double? FSV_Shares { get; set; }
		
		
		public double? FSV_Vehicle { get; set; }
		
		
		public double? OverlaysPercentage { get; set; }
		
		
		[Required]
		public string Reason { get; set; }
		
		
		[Required]
		public string ContractId { get; set; }
		
		
		 public Guid? ObeEclDataLoanBookId { get; set; }
		 
		 
    }
}