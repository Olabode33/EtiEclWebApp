using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TestDemo.EclShared;

namespace TestDemo.Dto
{
    public class CreateOrEditEclOverrideDtoBase : EntityDto<Guid?>
	{
		public int? Stage { get; set; }
		public double? ImpairmentOverride { get; set; }
		[Required]
		public string OverrideComment { get; set; }
		[Required]
		public string OverrideType { get; set; }
		public GeneralStatusEnum Status { get; set; }
		public Guid EclId { get; set; }
		public Guid RecordId { get; set; }
	}
}
