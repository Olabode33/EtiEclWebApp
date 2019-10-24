using TestDemo.EclShared;
using TestDemo.EclShared;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;

namespace TestDemo.EclShared
{
	[Table("LgdInputAssumptions")]
    [Audited]
    public class LgdInputAssumption : FullAuditedEntity<Guid> 
    {

		public virtual string Key { get; set; }
		
		public virtual string InputName { get; set; }
		
		public virtual string Value { get; set; }
		
		public virtual DataTypeEnum DataType { get; set; }
		
		public virtual bool IsComputed { get; set; }
		
		public virtual LdgInputAssumptionEnum LgdGroup { get; set; }
		
		public virtual bool RequiresGroupApproval { get; set; }

        public virtual FrameworkEnum Framework { get; set; }


    }
}