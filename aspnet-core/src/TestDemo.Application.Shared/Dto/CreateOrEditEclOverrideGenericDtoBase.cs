using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TestDemo.Dto
{
    public class CreateOrEditEclOverrideGenericDtoBase: CreateOrEditEclOverrideDtoBase
    {
		public int? TtrYears { get; set; }
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
		public string ContractId { get; set; }

	}
}
