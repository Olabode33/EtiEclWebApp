﻿using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestDemo.EclShared.Dtos
{
    public class GetAllAffiliateAssumptionDto: EntityDto<Guid>
    {
        public long OrganizationUnitId { get; set; }
        public string AffiliateName { get; set; }
        public DateTime LastAssumptionUpdate { get; set; }
        public DateTime LastWholesaleReportingDate { get; set; }
        public DateTime LastRetailReportingDate { get; set; }
        public DateTime LastObeReportingDate { get; set; }
        public DateTime LastSecuritiesReportingDate { get; set; }
        public bool RequiresAttention { get; set; }
        public bool HasSubmittedAssumptions { get; set; }
        public virtual GeneralStatusEnum Status { get; set; }
    }
}
