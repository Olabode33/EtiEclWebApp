using TestDemo.Wholesale;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;

namespace TestDemo.WholesaleComputation
{
	[Table("WholesaleEadCirProjections")]
    [Audited]
    public class WholesaleEadCirProjection : Entity<Guid> 
    {

		public virtual string CIR_GROUP { get; set; }
		
		public virtual int Months { get; set; }
		
		public virtual double Value { get; set; }
		
		public virtual double CIR_EFFECTIVE { get; set; }
		

		public virtual Guid? WholesaleEclId { get; set; }
		
        [ForeignKey("WholesaleEclId")]
		public WholesaleEcl WholesaleEclFk { get; set; }
		
    }
}