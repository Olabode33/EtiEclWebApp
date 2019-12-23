using TestDemo.EclShared;

using System;
using Abp.Application.Services.Dto;

namespace TestDemo.EclShared.Dtos
{
    public class PdInputAssumptionNonInternalModelDto : EntityDto<Guid>
    {
        public string Key { get; set; }
        public int Month { get; set; }
        public string PdGroup { get; set; }
        public double MarginalDefaultRate { get; set; }
        public double CummulativeSurvival { get; set; }
        public bool IsComputed { get; set; }
        public bool RequiresGroupApproval { get; set; }
        public bool CanAffiliateEdit { get; set; }
        public long OrganizationUnitId { get; set; }
        public DateTime? LastUpdated { get; set; }
        public string LastUpdatedBy { get; set; }
        public GeneralStatusEnum Status { get; set; }
    }
}