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
		public virtual int? StageOverride { get; set; }
		public virtual double? ImpairmentOverride { get; set; }
		public virtual string OverrideComment { get; set; }
		public virtual GeneralStatusEnum Status { get; set; }
	}
}
