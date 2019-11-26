using TestDemo.Wholesale;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace TestDemo.WholesaleComputation
{
	[Table("WholesalePdLifetimeDownturns")]
    public class WholesalePdLifetimeDownturn : Entity<Guid> 
    {

		public virtual string PdGroup { get; set; }
		
		public virtual int Month { get; set; }
		
		public virtual double Value { get; set; }
		

		public virtual Guid? WholesaleEclId { get; set; }
		
        [ForeignKey("WholesaleEclId")]
		public WholesaleEcl WholesaleEclFk { get; set; }
		
    }
}