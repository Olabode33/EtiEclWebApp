using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Text;
using TestDemo.EclShared;

namespace TestDemo.EclLibrary.BaseEngine.ComputationBase
{
    public class EclOverrideBase: FullAuditedEntity<Guid>
	{
		public virtual string ContractId { get; set; }
		public virtual int? Stage { get; set; }
		public virtual double? TtrYears { get; set; }
		public virtual double? FSV_Cash { get; set; }
		public virtual double? FSV_CommercialProperty { get; set; }
		public virtual double? FSV_Debenture { get; set; }
		public virtual double? FSV_Inventory { get; set; }
		public virtual double? FSV_PlantAndEquipment { get; set; }
		public virtual double? FSV_Receivables { get; set; }
		public virtual double? FSV_ResidentialProperty { get; set; }
		public virtual double? FSV_Shares { get; set; }
		public virtual double? FSV_Vehicle { get; set; }
		public virtual double? OverlaysPercentage { get; set; }
		public virtual string Reason { get; set; }
		public virtual GeneralStatusEnum Status { get; set; }
	}
}
