using TestDemo.EclShared;
using TestDemo.OBE;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;

namespace TestDemo.ObeAssumption
{
	[Table("ObeEclPdAssumptionNplIndexes")]
    [Audited]
    public class ObeEclPdAssumptionNplIndex : FullAuditedEntity<Guid> 
    {

		public virtual string Key { get; set; }
		
		public virtual DateTime Date { get; set; }
		
		public virtual double Actual { get; set; }
		
		public virtual double Standardised { get; set; }
		
		public virtual double EtiNplSeries { get; set; }
		
		public virtual GeneralStatusEnum Statue { get; set; }
		
		public virtual bool IsComputed { get; set; }
		
		public virtual bool CanAffiliateEdit { get; set; }
		
		public virtual bool RequiresGroupApproval { get; set; }
		
		public virtual long OrganizationUnitId { get; set; }
		

		public virtual Guid ObeEclId { get; set; }
		
        [ForeignKey("ObeEclId")]
		public ObeEcl ObeEclFk { get; set; }
		
    }
}