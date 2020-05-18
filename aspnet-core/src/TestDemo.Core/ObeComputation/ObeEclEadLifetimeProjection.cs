using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TestDemo.EclLibrary.BaseEngine.ComputationBase;
using TestDemo.OBE;

namespace TestDemo.ObeComputation
{
    public class ObeEclEadLifetimeProjection: EclEadLifetimeProjectionBase
    {
        public virtual Guid ObeEclId { get; set; }

        [ForeignKey("ObeEclId")]
        public ObeEcl ObeEclFk { get; set; }
    }
}
