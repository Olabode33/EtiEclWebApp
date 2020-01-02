using TestDemo.EclShared;
using TestDemo.EclShared;

using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.RetailAssumption.Dtos
{
    public class CreateOrEditRetailEclPdAssumptionMacroeconomicProjectionDto : EntityDto<Guid?>
    {

		 public Guid RetailEclId { get; set; }

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