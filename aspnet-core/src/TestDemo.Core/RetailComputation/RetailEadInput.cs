using TestDemo.Retail;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace TestDemo.RetailComputation
{
	[Table("RetailEadInputs")]
    public class RetailEadInput : Entity<Guid> 
    {

		public virtual string ContractId { get; set; }
		
		public virtual string EIR_GROUP { get; set; }
		
		public virtual string CIR_GROUP { get; set; }
		
		public virtual int Months { get; set; }
		
		public virtual double Value { get; set; }
		

		public virtual Guid RetailEclId { get; set; }
		
        [ForeignKey("RetailEclId")]
		public RetailEcl RetailEclFk { get; set; }
		
    }
}