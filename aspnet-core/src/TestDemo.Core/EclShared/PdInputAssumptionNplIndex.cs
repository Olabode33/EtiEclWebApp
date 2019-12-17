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
	[Table("PdInputAssumptionNplIndexes")]
    [Audited]
    public class PdInputAssumptionNplIndex : FullAuditedEntity<Guid>, IMustHaveOrganizationUnit
    {

		public virtual string Key { get; set; }
		
		public virtual DateTime Date { get; set; }

        public virtual double Actual { get; set; }

        public virtual double Standardised { get; set; }

        public virtual double EtiNplSeries { get; set; }
		
		public virtual GeneralStatusEnum Status { get; set; }
		
		public virtual bool IsComputed { get; set; }
		
		public virtual bool CanAffiliateEdit { get; set; }
		
		public virtual bool RequiresGroupApproval { get; set; }
		
		public virtual FrameworkEnum Framework { get; set; }
		
		public virtual long OrganizationUnitId { get; set; }
		

    }
}