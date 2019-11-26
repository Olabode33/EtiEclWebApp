using TestDemo.Retail;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace TestDemo.RetailComputation
{
	[Table("RetailPdMappings")]
    public class RetailPdMapping : Entity<Guid> 
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
		

		public virtual Guid RetailEclId { get; set; }
		
        [ForeignKey("RetailEclId")]
		public RetailEcl RetailEclFk { get; set; }
		
    }
}