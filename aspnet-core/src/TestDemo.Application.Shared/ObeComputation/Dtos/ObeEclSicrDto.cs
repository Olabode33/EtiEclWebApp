using TestDemo.EclShared;

using System;
using Abp.Application.Services.Dto;

namespace TestDemo.ObeComputation.Dtos
{
    public class ObeEclSicrDto : EntityDto<Guid>
    {
		public int ComputedSICR { get; set; }

		public string OverrideSICR { get; set; }

		public string OverrideComment { get; set; }

		public GeneralStatusEnum Status { get; set; }


		 public Guid? ObeEclDataLoanBookId { get; set; }

		 
    }
}