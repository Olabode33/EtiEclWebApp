using System;
using System.Collections.Generic;
using System.Text;

namespace TestDemo.EclShared.Dtos
{
    public class GetAffiliateAssumptionInputDto
    {
        public long AffiliateOuId { get; set; }
        public FrameworkEnum Framework { get; set; }
    }
}
