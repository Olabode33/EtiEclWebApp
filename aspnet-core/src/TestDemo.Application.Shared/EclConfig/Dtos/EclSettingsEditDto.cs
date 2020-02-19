using TestDemo.EclShared;

using System;
using Abp.Application.Services.Dto;

namespace TestDemo.EclConfig.Dtos
{
    public class EclSettingsEditDto
    {
        public DateTime OverrideCutOffTime { get; set; }
        public int RequiredNumberOfApprovals { get; set; }
        public LoanBookSnapshotEditDto LoanBookSnapshot { get; set; }
        public PaymentScheduleEditDto PaymentSchedule { get; set; }
        public AssetBookSettingsEditDto AssetBook { get; set; }
    }
}