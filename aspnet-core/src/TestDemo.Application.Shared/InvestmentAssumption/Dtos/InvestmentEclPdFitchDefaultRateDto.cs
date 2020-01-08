using TestDemo.EclShared;

using System;
using Abp.Application.Services.Dto;

namespace TestDemo.InvestmentAssumption.Dtos
{
    public class InvestmentEclPdFitchDefaultRateDto : EntityDto<Guid>
    {
		public string Rating { get; set; }

		public int Year { get; set; }

		public double Value { get; set; }

		public GeneralStatusEnum Status { get; set; }


		 public Guid InvestmentEclId { get; set; }

		 
    }
}