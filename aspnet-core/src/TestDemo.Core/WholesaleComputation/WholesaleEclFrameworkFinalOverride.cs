﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TestDemo.EclLibrary.BaseEngine.ComputationBase;
using TestDemo.Wholesale;

namespace TestDemo.WholesaleComputation
{
    [Table("WholesaleECLFrameworkFinalOverride")]
    public class WholesaleEclFrameworkFinalOverride: EclFrameworkFinalBase
    {
        public virtual Guid WholesaleEclId { get; set; }
    }
}
