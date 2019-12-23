using TestDemo.EclShared;
using TestDemo.EclShared;
using TestDemo.EclShared;

using System;
using Abp.Application.Services.Dto;

namespace TestDemo.EclShared.Dtos
{
    public class PdInputAssumptionNplIndexDto : EntityDto<Guid>
    {
        public string Key { get; set; }
        public DateTime Date { get; set; }
        public double Actual { get; set; }
        public double Standardised { get; set; }
        public double EtiNplSeries { get; set; }
        public bool IsComputed { get; set; }
        public bool RequiresGroupApproval { get; set; }
        public bool CanAffiliateEdit { get; set; }
        public long OrganizationUnitId { get; set; }
        public DateTime? LastUpdated { get; set; }
        public string LastUpdatedBy { get; set; }
        public GeneralStatusEnum Status { get; set; }

    }
}