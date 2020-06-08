using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using TestDemo.EclShared;

namespace TestDemo.Dto.Assumptions
{
    public class CreateOrEditEclPdAssumptionNplIndexDtoBase: EntityDto<Guid?>
    {
        public string Key { get; set; }

        public DateTime Date { get; set; }

        public double Actual { get; set; }

        public double Standardised { get; set; }

        public double EtiNplSeries { get; set; }

        public GeneralStatusEnum Status { get; set; }

        public bool IsComputed { get; set; }

        public bool CanAffiliateEdit { get; set; }

        public bool RequiresGroupApproval { get; set; }

        public long OrganizationUnitId { get; set; }
    }
}
