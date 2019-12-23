using TestDemo.EclShared;
using TestDemo.EclShared;
using TestDemo.OBE;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;
using Abp.Organizations;

namespace TestDemo.ObeAssumption
{
	[Table("ObeEclPdAssumptionMacroeconomicInputses")]
    [Audited]
    public class ObeEclPdAssumptionMacroeconomicInputs : FullAuditedEntity<Guid>, IMustHaveOrganizationUnit
    {

		public virtual string Key { get; set; }
		
		public virtual string InputName { get; set; }
		
		public virtual double Value { get; set; }
		
		public virtual PdInputAssumptionMacroEconomicInputGroupEnum MacroeconomicGroup { get; set; }
		
		public virtual bool IsComputed { get; set; }
		
		public virtual bool CanAffiliateEdit { get; set; }
		
		public virtual bool RequiresGroupApproval { get; set; }
		
		public virtual GeneralStatusEnum Status { get; set; }
		
		public virtual long OrganizationUnitId { get; set; }
		

		public virtual Guid ObeEclId { get; set; }
		
        [ForeignKey("ObeEclId")]
		public ObeEcl ObeEclFk { get; set; }
		
    }
}