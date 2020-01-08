using TestDemo.EclShared;
using TestDemo.Investment;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;

namespace TestDemo.InvestmentAssumption
{
	[Table("InvestmentEclPdFitchDefaultRates")]
    [Audited]
    public class InvestmentEclPdFitchDefaultRate : FullAuditedEntity<Guid> 
    {

		public virtual string Key { get; set; }
		
		public virtual string Rating { get; set; }
		
		public virtual int Year { get; set; }
		
		public virtual double Value { get; set; }
		
		public virtual bool RequiresGroupApproval { get; set; }
		
		public virtual GeneralStatusEnum Status { get; set; }
		

		public virtual Guid InvestmentEclId { get; set; }
		
        [ForeignKey("InvestmentEclId")]
		public InvestmentEcl InvestmentEclFk { get; set; }
		
    }
}