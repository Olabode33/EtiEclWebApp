using TestDemo.EclShared;
using TestDemo.Wholesale;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;
using TestDemo.EclLibrary.BaseEngine.ComputationBase;

namespace TestDemo.WholesaleComputation
{
	[Table("WholesalePdLifetimeBests")]
    [Audited]
    public class WholesalePdLifetimeBest : EclPdLifetimeBase
	{
		public virtual Guid WholesaleEclId { get; set; }		
    }
}