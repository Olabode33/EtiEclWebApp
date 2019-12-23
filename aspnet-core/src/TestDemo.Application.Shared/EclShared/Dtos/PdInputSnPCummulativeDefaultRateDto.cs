
using System;
using Abp.Application.Services.Dto;

namespace TestDemo.EclShared.Dtos
{
    public class PdInputSnPCummulativeDefaultRateDto : EntityDto<Guid>
    {

        public string Key { get; set; }
        public string Rating { get; set; }

		public int? Years { get; set; }

		public double? Value { get; set; }

        public bool IsComputed { get; set; }
        public bool RequiresGroupApproval { get; set; }
        public bool CanAffiliateEdit { get; set; }
        public long OrganizationUnitId { get; set; }
        public DateTime? LastUpdated { get; set; }
        public string LastUpdatedBy { get; set; }
        public GeneralStatusEnum Status { get; set; }

    }
}