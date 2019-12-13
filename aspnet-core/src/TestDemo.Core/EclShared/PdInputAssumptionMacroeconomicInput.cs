using TestDemo.EclShared;
using TestDemo.EclShared;
using TestDemo.EclShared;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;
using Abp.Organizations;

namespace TestDemo.EclShared
{
	[Table("PdInputAssumptionMacroeconomicInputs")]
    [Audited]
    public class PdInputAssumptionMacroeconomicInput : FullAuditedEntity<Guid>, IMustHaveOrganizationUnit
    {

		public virtual string Key { get; set; }
		
		public virtual string InputName { get; set; }
		
		public virtual double Value { get; set; }
		
		public virtual PdInputAssumptionMacroEconomicInputGroupEnum MacroEconomicInputGroup { get; set; }
		
		public virtual string IsComputed { get; set; }

        public virtual bool CanAffiliateEdit { get; set; }

        public virtual bool RequiresGroupApproval { get; set; }

        public virtual GeneralStatusEnum Status { get; set; }
		
		public virtual FrameworkEnum Framework { get; set; }
        public long OrganizationUnitId { get; set; }
    }
}