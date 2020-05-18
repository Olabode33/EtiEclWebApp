using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestDemo.EclLibrary.BaseEngine.ComputationBase
{
    public class EclEadInputBase: Entity<Guid>
    {
		public virtual string ContractId { get; set; }

		public virtual string EIR_GROUP { get; set; }

		public virtual string CIR_GROUP { get; set; }

		public virtual int Months { get; set; }

		public virtual double Value { get; set; }
	}
}
