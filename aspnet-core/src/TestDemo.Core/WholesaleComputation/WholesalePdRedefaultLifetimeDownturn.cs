using TestDemo.Wholesale;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using TestDemo.EclLibrary.BaseEngine.ComputationBase;

namespace TestDemo.WholesaleComputation
{
	[Table("WholesalePdRedefaultLifetimeDownturns")]
    public class WholesalePdRedefaultLifetimeDownturn : EclPdRedefaultLifetimeBase
	{
		public virtual Guid? WholesaleEclId { get; set; }		
    }
}