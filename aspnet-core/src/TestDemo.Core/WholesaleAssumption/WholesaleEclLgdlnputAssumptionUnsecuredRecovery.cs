using Abp.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TestDemo.EclLibrary.BaseEngine.AssumptionBase;
using TestDemo.Wholesale;

namespace TestDemo.WholesaleAssumption
{
    [Table("WholesaleLgdInputAssumptions_UnsecuredRecovery")]
    [Audited]
    public class WholesaleEclLgdlnputAssumptionUnsecuredRecovery: EclLgdInputAssumptionUnsecuredRecoveryBase
    {
        public virtual Guid WholesaleEclId { get; set; }

        [ForeignKey("WholesaleEclId")]
        public WholesaleEcl WholesaleEclFk { get; set; }
    }
}
