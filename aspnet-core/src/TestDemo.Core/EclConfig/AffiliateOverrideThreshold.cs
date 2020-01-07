using Abp.Organizations;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;

namespace TestDemo.EclConfig
{
	[Table("AffiliateOverrideThresholds")]
    [Audited]
    public class AffiliateOverrideThreshold : FullAuditedEntity 
    {

		public virtual double Threshold { get; set; }
		

		public virtual long OrganizationUnitId { get; set; }
		
        [ForeignKey("OrganizationUnitId")]
		public OrganizationUnit OrganizationUnitFk { get; set; }
		
    }
}