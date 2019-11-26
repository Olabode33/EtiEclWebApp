using TestDemo.Wholesale;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;

namespace TestDemo.WholesaleComputation
{
	[Table("WholesaleLgdContractDatas")]
    [Audited]
    public class WholesaleLgdContractData : Entity<Guid> 
    {

		public virtual string CONTRACT_NO { get; set; }
		
		public virtual double TTR_YEARS { get; set; }
		
		public virtual double COST_OF_RECOVERY { get; set; }
		
		public virtual double GUARANTOR_PD { get; set; }
		
		public virtual double GUARANTOR_LGD { get; set; }
		
		public virtual double GUARANTEE_VALUE { get; set; }
		
		public virtual double GUARANTEE_LEVEL { get; set; }
		

		public virtual Guid WholesaleEclId { get; set; }
		
        [ForeignKey("WholesaleEclId")]
		public WholesaleEcl WholesaleEclFk { get; set; }
		
    }
}