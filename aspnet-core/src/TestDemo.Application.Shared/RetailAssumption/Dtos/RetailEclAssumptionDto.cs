using TestDemo.EclShared;
using TestDemo.EclShared;

using System;
using Abp.Application.Services.Dto;

namespace TestDemo.RetailAssumption.Dtos
{
    public class RetailEclAssumptionDto : EntityDto<Guid>
    {
		public string Key { get; set; }

		public string InputName { get; set; }

		public string Value { get; set; }

		public DataTypeEnum Datatype { get; set; }

		public bool IsComputed { get; set; }

		public AssumptionGroupEnum AssumptionGroup { get; set; }

		public bool RequiresGroupApproval { get; set; }


		 public Guid? RetailEclId { get; set; }

		 
    }
}