using TestDemo.EclShared;
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
	[Table("InvestmentEclEadInputAssumptions")]
    [Audited]
    public class InvestmentEclEadInputAssumption : FullAuditedEntity<Guid> 
    {

		public virtual string Key { get; set; }
		
		public virtual string InputName { get; set; }
		
		public virtual string Value { get; set; }
		
		public virtual DataTypeEnum DataType { get; set; }
		
		public virtual bool IsComputed { get; set; }
		
		public virtual EadInputAssumptionGroupEnum EadGroup { get; set; }
		
		public virtual bool RequiresGroupApproval { get; set; }
		
		public virtual bool CanAffiliateEdit { get; set; }
		

		public virtual Guid? InvestmentEclId { get; set; }
		
        [ForeignKey("InvestmentEclId")]
		public InvestmentEcl InvestmentEclFk { get; set; }
		
    }
}