using TestDemo.EclShared;

using System;
using Abp.Application.Services.Dto;

namespace TestDemo.InvestmentComputation.Dtos
{
    public class InvestmentEclOverrideDto : EntityDto<Guid>
    {
		public int StageOverride { get; set; }

		public string OverrideComment { get; set; }

		public GeneralStatusEnum Status { get; set; }


		 public Guid InvestmentEclSicrId { get; set; }

		 
    }
}