using TestDemo.EclShared;

using System;
using Abp.Application.Services.Dto;

namespace TestDemo.WholesaleComputation.Dtos
{
    public class WholesaleEclSicrApprovalDto : EntityDto<Guid>
    {
		public DateTime? ReviewedDate { get; set; }

		public string ReviewComment { get; set; }

		public GeneralStatusEnum Status { get; set; }


		 public long? ReviewedByUserId { get; set; }

		 		 public Guid? WholesaleEclSicrId { get; set; }

		 
    }
}