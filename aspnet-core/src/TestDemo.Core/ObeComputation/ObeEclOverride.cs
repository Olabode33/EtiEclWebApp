using TestDemo.ObeInputs;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace TestDemo.ObeComputation
{
	[Table("ObeEclOverrides")]
    public class ObeEclOverride : FullAuditedEntity<Guid> 
    {

		public virtual double? Stage { get; set; }
		
		public virtual double? TtrYears { get; set; }
		
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
		

		public virtual Guid? ObeEclDataLoanBookId { get; set; }
		
        [ForeignKey("ObeEclDataLoanBookId")]
		public ObeEclDataLoanBook ObeEclDataLoanBookFk { get; set; }
		
    }
}