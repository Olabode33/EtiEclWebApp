
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;
using TestDemo.EclShared;

namespace TestDemo.RetailAssumption.Dtos
{
    public class CreateOrEditRetailEclPdAssumptionNonInteralModelDto : EntityDto<Guid?>
    {

		 public Guid RetailEclId { get; set; }

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