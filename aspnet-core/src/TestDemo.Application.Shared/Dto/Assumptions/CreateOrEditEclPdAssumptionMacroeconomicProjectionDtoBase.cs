using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using TestDemo.EclShared;

namespace TestDemo.Dto.Assumptions
{
    public class CreateOrEditEclPdAssumptionMacroeconomicProjectionDtoBase : EntityDto<Guid?>
    {
        public string Key { get; set; }

        public DateTime Date { get; set; }

        public string InputName { get; set; }

        public double BestValue { get; set; }

        public double OptimisticValue { get; set; }

        public double DownturnValue { get; set; }

        public int MacroeconomicVariableId { get; set; }

        public bool IsComputed { get; set; }

        public bool CanAffiliateEdit { get; set; }

        public GeneralStatusEnum Status { get; set; }

        public long OrganizationUnitId { get; set; }
    }
}
