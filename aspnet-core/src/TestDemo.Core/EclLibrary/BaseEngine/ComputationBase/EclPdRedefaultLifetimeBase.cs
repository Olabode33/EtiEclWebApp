using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestDemo.EclLibrary.BaseEngine.ComputationBase
{
    public class EclPdRedefaultLifetimeBase : Entity<Guid>
    {
        public virtual string PdGroup { get; set; }
        public virtual int Month { get; set; }
        public virtual double Value { get; set; }
    }
}
