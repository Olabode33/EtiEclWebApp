using TestDemo.EclShared;
using TestDemo.EclShared;
using TestDemo.EclShared;

using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.RetailAssumption.Dtos
{
    public class CreateOrEditRetailEclPdAssumptionDto : EntityDto<Guid?>
    {

        public Guid RetailEclId { get; set; }

        public string Key { get; set; }

        public string InputName { get; set; }

        public string Value { get; set; }

        public DataTypeEnum DataType { get; set; }

        public PdInputAssumptionGroupEnum PdGroup { get; set; }

        public GeneralStatusEnum Status { get; set; }

        public bool IsComputed { get; set; }

        public bool CanAffiliateEdit { get; set; }

        public bool RequiresGroupApproval { get; set; }

        public long OrganizationUnitId { get; set; }
    }
}