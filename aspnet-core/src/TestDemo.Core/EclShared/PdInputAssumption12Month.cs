using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;

namespace TestDemo.EclShared
{
	[Table("PdInputAssumption12Months")]
    [Audited]
    public class PdInputAssumption12Month : FullAuditedEntity<Guid> 
    {

		public virtual int Credit { get; set; }
		
		public virtual double? PD { get; set; }
		
		public virtual string SnPMappingEtiCreditPolicy { get; set; }
		
		public virtual string SnPMappingBestFit { get; set; }
		
		public virtual bool RequiresGroupApproval { get; set; }
		

    }
}