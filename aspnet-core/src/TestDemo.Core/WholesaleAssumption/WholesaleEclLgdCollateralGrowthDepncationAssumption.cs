using Abp.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TestDemo.EclLibrary.BaseEngine.AssumptionBase;
using TestDemo.Wholesale;

namespace TestDemo.WholesaleAssumption
{
    [Table("WholesaleLgdCollateralGrowth_DepricationAssumption")]
    [Audited]
    public class WholesaleEclLgdCollateralGrowthDepncationAssumption: EclLgdCollateralGrowthDepricationAssumptionBase
    {
        public virtual Guid WholesaleEclId { get; set; }

        [ForeignKey("WholesaleEclId")]
        public WholesaleEcl WholesaleEclFk { get; set; }

    }
}
