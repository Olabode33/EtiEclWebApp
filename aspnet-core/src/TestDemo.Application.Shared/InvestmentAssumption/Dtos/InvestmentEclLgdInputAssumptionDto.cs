using TestDemo.EclShared;
using TestDemo.EclShared;

using System;
using Abp.Application.Services.Dto;

namespace TestDemo.InvestmentAssumption.Dtos
{
    public class InvestmentEclLgdInputAssumptionDto : EntityDto<Guid>
    {
		public string InputName { get; set; }

		public string Value { get; set; }


		 public Guid InvestmentEclId { get; set; }

		 
    }
}