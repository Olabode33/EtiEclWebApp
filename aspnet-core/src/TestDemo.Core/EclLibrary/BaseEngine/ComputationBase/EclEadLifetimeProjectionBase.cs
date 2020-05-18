using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TestDemo.EclLibrary.BaseEngine.ComputationBase
{
    public class EclEadLifetimeProjectionBase: Entity<Guid>
    {
		public virtual string Contract_no { get; set; }
		public virtual string Eir_Group { get; set; }
		public virtual string Cir_Group { get; set; }
		[Required]
		public virtual int Month { get; set; }
		[Required]
		public virtual double Value { get; set; }
	}
}
