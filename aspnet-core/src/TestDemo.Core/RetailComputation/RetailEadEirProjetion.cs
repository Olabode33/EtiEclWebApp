using TestDemo.Retail;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace TestDemo.RetailComputation
{
	[Table("RetailEadEirProjetions")]
    public class RetailEadEirProjection : Entity<Guid> 
    {

		public virtual string EIR_Group { get; set; }
		
		public virtual int Month { get; set; }
		
		public virtual double Value { get; set; }
		

		public virtual Guid RetailEclId { get; set; }
		
        [ForeignKey("RetailEclId")]
		public RetailEcl RetailEclFk { get; set; }
		
    }
}