using Abp.Organizations;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestDemo.EclConfig
{
    public class AffiliateConfiguration: OrganizationUnit
    {
        public double? OverrideThreshold { get; set; }
    }
}
