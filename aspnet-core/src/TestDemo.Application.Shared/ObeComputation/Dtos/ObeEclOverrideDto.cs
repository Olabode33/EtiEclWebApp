
using System;
using Abp.Application.Services.Dto;

namespace TestDemo.ObeComputation.Dtos
{
    public class ObeEclOverrideDto : EntityDto<Guid>
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

		public string Reason { get; set; }

		public string ContractId { get; set; }


		 public Guid? ObeEclDataLoanBookId { get; set; }

		 
    }
}