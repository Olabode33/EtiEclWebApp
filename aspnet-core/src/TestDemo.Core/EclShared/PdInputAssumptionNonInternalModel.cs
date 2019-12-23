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
	[Table("PdInputAssumptionNonInternalModels")]
    [Audited]
    public class PdInputAssumptionNonInternalModel : FullAuditedEntity<Guid>, IMustHaveOrganizationUnit
    {

		public virtual string Key { get; set; }
		
		public virtual int Month { get; set; }
		
		public virtual string PdGroup { get; set; }
		
		public virtual double MarginalDefaultRate { get; set; }
		
		public virtual double CummulativeSurvival { get; set; }
		
		public virtual bool IsComputed { get; set; }

        public virtual bool CanAffiliateEdit { get; set; }

        public virtual bool RequiresGroupApproval { get; set; }

        public virtual FrameworkEnum Framework { get; set; }

        public long OrganizationUnitId { get; set; }

        public virtual GeneralStatusEnum Status { get; set; }
    }
}