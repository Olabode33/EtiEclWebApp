using TestDemo.EclShared;
using TestDemo.EclShared;

using System;
using Abp.Application.Services.Dto;

namespace TestDemo.WholesaleAssumption.Dtos
{
    public class WholesaleEadInputAssumptionDto : EntityDto<Guid>
    {
		public string InputName { get; set; }

		public string Value { get; set; }

		public EadInputAssumptionGroupEnum EadGroup { get; set; }


		 public Guid WholesaleEclId { get; set; }

		 
    }
}