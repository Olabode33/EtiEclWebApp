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
	[Table("ObeEclPdAssumptionMacroeconomicProjections")]
    [Audited]
    public class ObeEclPdAssumptionMacroeconomicProjection : FullAuditedEntity<Guid> 
    {

		public virtual string Key { get; set; }
		
		public virtual DateTime Date { get; set; }
		
		public virtual string InputName { get; set; }
		
		public virtual double BestValue { get; set; }
		
		public virtual double OptimisticValue { get; set; }
		
		public virtual double DownturnValue { get; set; }
		
		public virtual PdInputAssumptionMacroEconomicInputGroupEnum MacroeconomicGroup { get; set; }
		
		public virtual bool IsComputed { get; set; }
		
		public virtual bool CanAffiliateEdit { get; set; }
		
		public virtual bool RequiresGroupApproval { get; set; }
		
		public virtual long OrganizationUnitId { get; set; }
		

		public virtual Guid ObeEclId { get; set; }
		
        [ForeignKey("ObeEclId")]
		public ObeEcl ObeEclFk { get; set; }
		
    }
}