using TestDemo.EclShared;
using TestDemo.EclShared;

using System;
using Abp.Application.Services.Dto;

namespace TestDemo.WholesaleAssumption.Dtos
{
    public class WholesaleEclAssumptionDto : EntityDto<Guid>
    {
		public string InputName { get; set; }

		public string Value { get; set; }

		public bool IsComputed { get; set; }

		public AssumptionGroupEnum AssumptionGroup { get; set; }


		 public Guid WholesaleEclId { get; set; }

		 
    }
}