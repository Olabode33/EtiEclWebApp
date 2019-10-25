using TestDemo.EclShared;

using System;
using Abp.Application.Services.Dto;

namespace TestDemo.RetailComputation.Dtos
{
    public class RetailEclSicrDto : EntityDto<Guid>
    {
		public int ComputedSICR { get; set; }

		public string OverrideSICR { get; set; }

		public string OverrideComment { get; set; }

		public GeneralStatusEnum Status { get; set; }


		 public Guid RetailEclDataLoanBookId { get; set; }

		 
    }
}