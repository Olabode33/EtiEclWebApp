using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TestDemo.EclLibrary.BaseEngine.ComputationBase;
using TestDemo.Wholesale;

namespace TestDemo.WholesaleComputation
{
    [Table("WholesalePDCreditIndex")]
    public class WholesaleEcIPdCreditIndex: EclPdCreditIndexBase
    {
        public virtual Guid WholesaleEclId { get; set; }

        [ForeignKey("WholesaleEclId")]
        public WholesaleEcl WholesaleEclFk { get; set; }
    }
}
