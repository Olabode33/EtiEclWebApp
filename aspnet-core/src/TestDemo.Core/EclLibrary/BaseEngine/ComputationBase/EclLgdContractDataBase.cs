using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestDemo.EclLibrary.BaseEngine.ComputationBase
{
    public class EclLgdContractDataBase : Entity<Guid>
    {
		public virtual string CONTRACT_NO { get; set; }
		public virtual double TTR_YEARS { get; set; }
		public virtual double COST_OF_RECOVERY { get; set; }
		public virtual double GUARANTOR_PD { get; set; }
		public virtual double GUARANTOR_LGD { get; set; }
		public virtual double GUARANTEE_VALUE { get; set; }
		public virtual double GUARANTEE_LEVEL { get; set; }
	}
}
