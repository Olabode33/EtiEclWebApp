using TestDemo.OBE;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace TestDemo.ObeComputation
{
	[Table("ObePdMappings")]
    public class ObePdMapping : Entity<Guid> 
    {

		public virtual string ContractId { get; set; }
		
		public virtual string PdGroup { get; set; }
		
		public virtual int TtmMonths { get; set; }
		
		public virtual int MaxDpd { get; set; }
		
		public virtual int MaxClassificationScore { get; set; }
		
		public virtual double Pd12Month { get; set; }
		
		public virtual double LifetimePd { get; set; }
		
		public virtual double RedefaultLifetimePd { get; set; }
		
		public virtual int Stage1Transition { get; set; }
		
		public virtual int Stage2Transition { get; set; }
		
		public virtual int DaysPastDue { get; set; }
		

		public virtual Guid? ObeEclId { get; set; }
		
        [ForeignKey("ObeEclId")]
		public ObeEcl ObeEclFk { get; set; }
		
    }
}