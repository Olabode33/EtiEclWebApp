using TestDemo.EclShared;

using System;
using Abp.Application.Services.Dto;

namespace TestDemo.InvestmentAssumption.Dtos
{
    public class InvestmentPdInputMacroEconomicAssumptionDto : EntityDto<Guid>
    {
		public int Month { get; set; }

		public double BestValue { get; set; }

		public double OptimisticValue { get; set; }

		public double DownturnValue { get; set; }

		public GeneralStatusEnum Status { get; set; }


		 public Guid InvestmentEclId { get; set; }

		 
    }
}