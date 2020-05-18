using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestDemo.EclLibrary.BaseEngine.ComputationBase
{
    public class EclPdMappingBase : Entity<Guid>
    {
		public virtual string ContractId { get; set; }
		public virtual string PdGroup { get; set; }
		public virtual int TtmMonths { get; set; }
		public virtual int MaxDpd { get; set; }
		public virtual int MaxClassificationScore { get; set; }
		public virtual double Pd12Month { get; set; }
		public virtual double LifetimePd { get; set; }
		public virtual double RedefaultLifetimePd { get; set; }
		public virtual int Stage1Transition { get; set; }
		public virtual int Stage2Transition { get; set; }
		public virtual int DaysPastDue { get; set; }
	}
}
