using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TestDemo.EclLibrary.BaseEngine.ComputationBase;
using TestDemo.Wholesale;

namespace TestDemo.WholesaleComputation
{
    [Table("WholesaleLgdCollateralProjection")]
    public class WholesaleEclLgdCollateralProjection: EclLgdCollateralProjectionBase
    {
        public virtual Guid WholesaleEclId { get; set; }
    }
}
