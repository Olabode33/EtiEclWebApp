using TestDemo.EclShared;
using TestDemo.EclShared;

using System;
using Abp.Application.Services.Dto;

namespace TestDemo.EclShared.Dtos
{
    public class EadInputAssumptionDto : EntityDto<Guid>
    {
        public string Key { get; set; }
		public string InputName { get; set; }
		public string Value { get; set; }
        public DataTypeEnum DataType { get; set; }
        public EadInputAssumptionGroupEnum AssumptionGroup { get; set; }
        public bool IsComputed { get; set; }
        public bool RequiresGroupApproval { get; set; }
        public bool CanAffiliateEdit { get; set; }
        public long OrganizationUnitId { get; set; }
        public DateTime? LastUpdated { get; set; }
        public string LastUpdatedBy { get; set; }
        public GeneralStatusEnum Status { get; set; }
    }
}