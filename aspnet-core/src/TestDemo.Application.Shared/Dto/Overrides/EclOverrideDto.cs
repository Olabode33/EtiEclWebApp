using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using TestDemo.EclShared;

namespace TestDemo.Dto.Overrides
{
    public class EclOverrideDto : EntityDto<Guid>
	{
		public int? StageOverride { get; set; }
		public double? ImpairmentOverride { get; set; }
		public string OverrideComment { get; set; }
		public GeneralStatusEnum Status { get; set; }
		public Guid EclId { get; set; }
		public Guid RecordId { get; set; }
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
		public string OverrideType { get; set; }
	}
}
