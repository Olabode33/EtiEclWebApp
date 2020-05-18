using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestDemo.EclLibrary.BaseEngine.ComputationBase
{
    public class EclEadEirProjectionBase: Entity<Guid>
    {
        public string EIR_Group { get; set; }

        public int Month { get; set; }

        public double Value { get; set; }
    }
}
