using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;

namespace TestDemo.EclShared
{
	[Table("PdInputSnPCummulativeDefaultRates")]
    [Audited]
    public class PdInputSnPCummulativeDefaultRate : FullAuditedEntity<Guid> 
    {

		public virtual string Key { get; set; }
		
		public virtual string Rating { get; set; }
		
		public virtual int? Years { get; set; }
		
		public virtual double? Value { get; set; }
		
		public virtual bool RequiresGroupApproval { get; set; }
		

    }
}