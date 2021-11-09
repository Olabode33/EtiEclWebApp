using Abp.Organizations;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestDemo.EclConfig
{
    public class Affiliate: OrganizationUnit
    {
        public double? OverrideThreshold { get; set; }
        public string Currency { get; set; }
        public long? SourceAffiliateId { get; set; }
        public bool? CalibrationFilesCreated { get; set; }
    }
}
