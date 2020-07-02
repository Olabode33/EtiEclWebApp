using TestDemo.EclShared;
using TestDemo.InvestmentComputation;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;
using TestDemo.Investment;

namespace TestDemo.InvestmentComputation
{
	[Table("InvestmentEclOverrides")]
    [Audited]
    public class InvestmentEclOverride : FullAuditedEntity<Guid> 
    {
		public virtual int? StageOverride { get; set; }
		public virtual double? ImpairmentOverride { get; set; }
		
		[Required]
		public virtual string OverrideComment { get; set; }
		[Required]
		public virtual string OverrideType { get; set; }
		
		public virtual GeneralStatusEnum Status { get; set; }

		public virtual Guid EclId { get; set; }

		public virtual Guid InvestmentEclSicrId { get; set; }
		
        [ForeignKey("InvestmentEclSicrId")]
		public InvestmentEclSicr InvestmentEclSicrFk { get; set; }
		
    }
}