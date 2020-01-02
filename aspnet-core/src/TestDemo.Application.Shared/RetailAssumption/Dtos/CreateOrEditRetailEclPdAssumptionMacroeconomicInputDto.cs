using TestDemo.EclShared;
using TestDemo.EclShared;

using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.RetailAssumption.Dtos
{
    public class CreateOrEditRetailEclPdAssumptionMacroeconomicInputDto : EntityDto<Guid?>
    {

		 public Guid RetailEclId { get; set; }

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