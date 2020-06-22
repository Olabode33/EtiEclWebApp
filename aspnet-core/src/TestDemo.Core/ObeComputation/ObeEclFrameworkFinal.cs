using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TestDemo.EclLibrary.BaseEngine.ComputationBase;
using TestDemo.OBE;

namespace TestDemo.ObeComputation
{
    [Table("ObeECLFrameworkFinal")]
    public class ObeEclFrameworkFinal: EclFrameworkFinalBase
    {
        public virtual Guid ObeEclId { get; set; }
    }
}
