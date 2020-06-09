using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using TestDemo.EclShared;

namespace TestDemo.Dto.Overrides
{
    public class EclOverrideDto : EntityDto<Guid>
	{
		public int? StageOverride { get; set; }

		public double? ImpairmentOverride { get; set; }

		public string OverrideComment { get; set; }

		public GeneralStatusEnum Status { get; set; }

		public Guid EclId { get; set; }

		public Guid RecordId { get; set; }

	}
}
