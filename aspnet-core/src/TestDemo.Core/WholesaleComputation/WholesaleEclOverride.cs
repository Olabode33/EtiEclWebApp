using TestDemo.WholesaleInputs;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace TestDemo.WholesaleComputation
{
	[Table("WholesaleEclOverrides")]
    public class WholesaleEclOverride : FullAuditedEntity<Guid> 
    {

		public virtual int? Stage { get; set; }
		
		public virtual int? TtrYears { get; set; }
		
		public virtual double? FSV_Cash { get; set; }
		
		public virtual double? FSV_CommercialProperty { get; set; }
		
		public virtual double? FSV_Debenture { get; set; }
		
		public virtual double? FSV_Inventory { get; set; }
		
		public virtual double? FSV_PlantAndEquipment { get; set; }
		
		public virtual double? FSV_Receivables { get; set; }
		
		public virtual double? FSV_ResidentialProperty { get; set; }
		
		public virtual double? FSV_Shares { get; set; }
		
		public virtual double? FSV_Vehicle { get; set; }
		
		public virtual double? OverlaysPercentage { get; set; }
		
		[Required]
		public virtual string Reason { get; set; }
		
		[Required]
		public virtual string ContractId { get; set; }
		

		public virtual Guid? WholesaleEclDataLoanBookId { get; set; }
		
        [ForeignKey("WholesaleEclDataLoanBookId")]
		public WholesaleEclDataLoanBook WholesaleEclDataLoanBookFk { get; set; }
		
    }
}