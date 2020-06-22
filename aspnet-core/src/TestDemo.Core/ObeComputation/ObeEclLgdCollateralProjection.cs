using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TestDemo.EclLibrary.BaseEngine.ComputationBase;
using TestDemo.OBE;

namespace TestDemo.ObeComputation
{
    [Table("ObeLgdCollateralProjection")]
    public class ObeEclLgdCollateralProjection: EclLgdCollateralProjectionBase
    {
        public virtual Guid ObeEclId { get; set; }

    }
}
