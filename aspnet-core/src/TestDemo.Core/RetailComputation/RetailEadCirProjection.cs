using TestDemo.Retail;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace TestDemo.RetailComputation
{
	[Table("RetailEadCirProjections")]
    public class RetailEadCirProjection : Entity<Guid> 
    {

		public virtual string CIR_GROUP { get; set; }
		
		public virtual int Months { get; set; }
		
		public virtual double Value { get; set; }
		
		public virtual string CIR_EFFECTIVE { get; set; }
		

		public virtual Guid? RetailEclId { get; set; }
		
        [ForeignKey("RetailEclId")]
		public RetailEcl RetailEclFk { get; set; }
		
    }
}