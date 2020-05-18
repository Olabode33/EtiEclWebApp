using Abp.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TestDemo.EclLibrary.BaseEngine.AssumptionBase;
using TestDemo.Retail;

namespace TestDemo.RetailAssumption
{
    [Table("RetailLgdCollateralGrowth_DepricationAssumption")]
    [Audited]
    public class RetailEclLgdCollateralGrowthDepricationAssumption: EclLgdCollateralGrowthDepricationAssumptionBase
    {
        public virtual Guid? RetailEclId { get; set; }

        [ForeignKey("RetailEclId")]
        public RetailEcl RetailEclFk { get; set; }
    }
}
