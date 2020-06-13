using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestDemo.EclShared.Dtos
{
    public class CreateOrEditAffiliateAssumptionsDto: EntityDto<Guid>
    {
        public DateTime LastAssumptionUpdate { get; set; }
        public DateTime LastWholesaleReportingDate { get; set; }
        public DateTime LastRetailReportingDate { get; set; }
        public DateTime LastObeReportingDate { get; set; }
        public DateTime LastSecuritiesReportingDate { get; set; }
        public long OrganizationUnitId { get; set; }
        public GeneralStatusEnum Status { get; set; }
    }
}
