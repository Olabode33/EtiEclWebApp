using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TestDemo.EclShared
{
    [Table("AffiliateAssumption")]
    public class AffiliateAssumption: FullAuditedEntity<Guid>, IMustHaveOrganizationUnit
    {
        public virtual DateTime LastAssumptionUpdate { get; set; }
        public virtual DateTime LastWholesaleReportingDate { get; set; }
        public virtual DateTime LastRetailReportingDate { get; set; }
        public virtual DateTime LastObeReportingDate { get; set; }
        public virtual DateTime LastSecuritiesReportingDate { get; set; }
        public virtual long OrganizationUnitId { get; set; }
        [ForeignKey("OrganizationUnitId")]
        public virtual OrganizationUnit OrganizationUnitFk { get; set; }
        public virtual GeneralStatusEnum Status { get; set; }
    }
}
