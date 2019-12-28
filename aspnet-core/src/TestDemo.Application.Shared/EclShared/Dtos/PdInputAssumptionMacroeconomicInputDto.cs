using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestDemo.EclShared.Dtos
{
    public class PdInputAssumptionMacroeconomicInputDto: EntityDto<Guid>
    {
        public string Key { get; set; }
        public string InputName { get; set; }
        public string MacroeconomicVariable { get; set; }
        public double Value { get; set; }
        public int AssumptionGroup { get; set; }
        public bool IsComputed { get; set; }
        public bool RequiresGroupApproval { get; set; }
        public bool CanAffiliateEdit { get; set; }
        public long OrganizationUnitId { get; set; }
        public DateTime? LastUpdated { get; set; }
        public string LastUpdatedBy { get; set; }
        public GeneralStatusEnum Status { get; set; }
    }
}
