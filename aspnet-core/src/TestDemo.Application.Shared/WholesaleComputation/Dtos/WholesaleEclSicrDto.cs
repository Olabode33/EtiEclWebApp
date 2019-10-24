using TestDemo.EclShared;

using System;
using Abp.Application.Services.Dto;

namespace TestDemo.WholesaleComputation.Dtos
{
    public class WholesaleEclSicrDto : EntityDto<Guid>
    {
		public int ComputedSICR { get; set; }

		public string OverrideSICR { get; set; }

		public string OverrideComment { get; set; }

		public GeneralStatusEnum Status { get; set; }


		 public Guid WholesaleEclDataLoanBookId { get; set; }

		 
    }
}