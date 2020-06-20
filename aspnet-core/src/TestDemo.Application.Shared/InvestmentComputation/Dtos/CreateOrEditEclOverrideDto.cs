using TestDemo.EclShared;

using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;
using TestDemo.Dto;

namespace TestDemo.InvestmentComputation.Dtos
{
    public class CreateOrEditEclOverrideDto : CreateOrEditEclOverrideDtoBase
    { 
		public Guid EclSicrId { get; set; }
        public string ContractId { get; set; }
    }

    public class CreateOrEditEclOverrideNewDto: EntityDto<Guid>
    {
		public virtual string ContractId { get; set; }
		public virtual int? Stage { get; set; }
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
		public virtual string OverrideComment { get; set; }
		public virtual GeneralStatusEnum Status { get; set; }
		public Guid EclId { get; set; }
	}
}