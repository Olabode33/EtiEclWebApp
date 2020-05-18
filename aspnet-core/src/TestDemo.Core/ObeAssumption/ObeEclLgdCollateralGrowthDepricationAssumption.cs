using Abp.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TestDemo.EclLibrary.BaseEngine.AssumptionBase;
using TestDemo.OBE;

namespace TestDemo.ObeAssumption
{
    [Table("ObeLgdCollateralGrowth_DepricationAssumption")]
    [Audited]
    public class ObeEclLgdCollateralGrowthDepricationAssumption: EclLgdCollateralGrowthDepricationAssumptionBase
    {
        public virtual Guid? ObeEclId { get; set; }

        [ForeignKey("ObeEclId")]
        public ObeEcl ObeEclFk { get; set; }
    }
}
