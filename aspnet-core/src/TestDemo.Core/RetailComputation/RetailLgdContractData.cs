using TestDemo.Retail;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace TestDemo.RetailComputation
{
	[Table("RetailLgdContractDatas")]
    public class RetailLgdContractData : Entity<Guid> 
    {

		public virtual string CONTRACT_NO { get; set; }
		
		public virtual double TTR_YEARS { get; set; }
		
		public virtual double COST_OF_RECOVERY { get; set; }
		
		public virtual double GUARANTOR_PD { get; set; }
		
		public virtual double GUARANTOR_LGD { get; set; }
		
		public virtual double GUARANTEE_VALUE { get; set; }
		
		public virtual double GUARANTEE_LEVEL { get; set; }
		

		public virtual Guid RetailEclId { get; set; }
		
        [ForeignKey("RetailEclId")]
		public RetailEcl RetailEclFk { get; set; }
		
    }
}