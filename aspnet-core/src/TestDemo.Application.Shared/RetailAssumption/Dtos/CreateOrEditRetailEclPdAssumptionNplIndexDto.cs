using TestDemo.EclShared;

using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.RetailAssumption.Dtos
{
    public class CreateOrEditRetailEclPdAssumptionNplIndexDto : EntityDto<Guid?>
    {

		 public Guid RetailEclId { get; set; }

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