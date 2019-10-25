
using System;
using Abp.Application.Services.Dto;

namespace TestDemo.ObeAssumption.Dtos
{
    public class ObeEclPdSnPCummulativeDefaultRateDto : EntityDto<Guid>
    {
		public string Key { get; set; }

		public string Rating { get; set; }

		public int? Years { get; set; }

		public double? Value { get; set; }

		public bool RequiresGroupApproval { get; set; }


		 public Guid? ObeEclId { get; set; }

		 
    }
}