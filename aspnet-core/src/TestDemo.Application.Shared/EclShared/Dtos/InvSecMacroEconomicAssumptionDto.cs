using TestDemo.EclShared;
using TestDemo.EclShared;

using System;
using Abp.Application.Services.Dto;

namespace TestDemo.EclShared.Dtos
{
    public class InvSecMacroEconomicAssumptionDto : EntityDto<Guid>
    {
		public int Month { get; set; }

		public double BestValue { get; set; }

		public double OptimisticValue { get; set; }

		public double DownturnValue { get; set; }

		public GeneralStatusEnum Status { get; set; }

		public long OrganizationUnitId { get; set; }



    }
}