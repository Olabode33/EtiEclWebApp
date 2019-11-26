using TestDemo.Wholesale;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace TestDemo.WholesaleComputation
{
	[Table("WholesaleEadInputs")]
    public class WholesaleEadInput : Entity<Guid> 
    {

		public virtual string ContractId { get; set; }
		
		public virtual string EIR_GROUP { get; set; }
		
		public virtual string CIR_GROUP { get; set; }
		
		public virtual int Months { get; set; }
		
		public virtual double Value { get; set; }
		

		public virtual Guid WholesaleEclId { get; set; }
		
        [ForeignKey("WholesaleEclId")]
		public WholesaleEcl WholesaleEclFk { get; set; }
		
    }
}