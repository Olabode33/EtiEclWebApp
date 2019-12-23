using TestDemo.EclShared;
using TestDemo.EclShared;
using TestDemo.EclShared;

using System;
using Abp.Application.Services.Dto;

namespace TestDemo.EclShared.Dtos
{
    public class PdInputAssumptionMacroeconomicProjectionDto : EntityDto<Guid>
    {
        public string Key { get; set; }
        public DateTime Date { get; set; }
        public string InputName { get; set; }
        public double BestValue { get; set; }
        public double OptimisticValue { get; set; }
        public double DownturnValue { get; set; }
        public PdInputAssumptionMacroEconomicInputGroupEnum AssumptionGroup { get; set; }
        public bool IsComputed { get; set; }
        public bool RequiresGroupApproval { get; set; }
        public bool CanAffiliateEdit { get; set; }
        public long OrganizationUnitId { get; set; }
        public DateTime? LastUpdated { get; set; }
        public string LastUpdatedBy { get; set; }
        public GeneralStatusEnum Status { get; set; }

    }
}