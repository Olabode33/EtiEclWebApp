using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using TestDemo.EclShared;

namespace TestDemo.Dto.Assumptions
{
    public class CreateOrEditEclPdAssumptionNonInteralModelDtoBase: EntityDto<Guid?>
    {
        public string Key { get; set; }

        public int Month { get; set; }

        public string PdGroup { get; set; }

        public double MarginalDefaultRate { get; set; }

        public double CummulativeSurvival { get; set; }

        public bool IsComputed { get; set; }

        public bool CanAffiliateEdit { get; set; }

        public bool RequiresGroupApproval { get; set; }

        public long OrganizationUnitId { get; set; }

        public GeneralStatusEnum Status { get; set; }
    }
}
