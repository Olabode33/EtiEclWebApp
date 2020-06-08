using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using TestDemo.EclShared;

namespace TestDemo.Dto.Assumptions
{
    public class CreateOrEditEclPdAssumptionMacroeconomicInputDtoBase: EntityDto<Guid?>
    {
        public string Key { get; set; }

        public string InputName { get; set; }

        public double Value { get; set; }

        public int MacroeconomicVariableId { get; set; }

        public bool IsComputed { get; set; }

        public bool CanAffiliateEdit { get; set; }

        public bool RequiresGroupApproval { get; set; }

        public GeneralStatusEnum Status { get; set; }
        public long OrganizationUnitId { get; set; }
    }
}
