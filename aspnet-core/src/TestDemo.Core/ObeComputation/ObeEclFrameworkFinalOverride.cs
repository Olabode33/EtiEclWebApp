using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TestDemo.EclLibrary.BaseEngine.ComputationBase;
using TestDemo.OBE;

namespace TestDemo.ObeComputation
{
    [Table("ObeECLFrameworkFinalOverride")]
    public class ObeEclFrameworkFinalOverride: EclFrameworkFinalBase
    {
        public virtual Guid ObeEclId { get; set; }
    }
}
