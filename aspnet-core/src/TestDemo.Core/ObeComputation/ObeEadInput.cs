using TestDemo.OBE;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace TestDemo.ObeComputation
{
	[Table("ObeEadInputs")]
    public class ObeEadInput : Entity<Guid> 
    {

		public virtual string ContractId { get; set; }
		
		public virtual string EIR_GROUP { get; set; }
		
		public virtual string CIR_GROUP { get; set; }
		
		public virtual int Months { get; set; }
		
		public virtual double Value { get; set; }
		

		public virtual Guid ObeEclId { get; set; }
		
        [ForeignKey("ObeEclId")]
		public ObeEcl ObeEclFk { get; set; }
		
    }
}