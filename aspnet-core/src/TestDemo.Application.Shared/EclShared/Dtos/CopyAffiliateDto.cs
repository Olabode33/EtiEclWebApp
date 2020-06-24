using System;
using System.Collections.Generic;
using System.Text;

namespace TestDemo.EclShared.Dtos
{
    public class CopyAffiliateDto
    {
        public long FromAffiliateId { get; set; }
        public long ToAffiliateId { get; set; }
    }

    public class ApplyAssumptionToAllAffiliateDto
    {
        public long FromAffiliateId { get; set; }
        public AssumptionTypeEnum Type { get; set; }
        public FrameworkEnum Framework { get; set; }
    }

    public class ApplyAssumptionToSelectedAffiliateDto
    {
        public long FromAffiliateId { get; set; }
        public long ToAffiliateId { get; set; }
        public AssumptionTypeEnum Type { get; set; }
        public FrameworkEnum Framework { get; set; }
    }
}
