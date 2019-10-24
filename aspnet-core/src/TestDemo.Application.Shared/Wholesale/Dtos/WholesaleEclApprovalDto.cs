using TestDemo.EclShared;

using System;
using Abp.Application.Services.Dto;

namespace TestDemo.Wholesale.Dtos
{
    public class WholesaleEclApprovalDto : EntityDto<Guid>
    {
		public DateTime? ReviewedDate { get; set; }

		public GeneralStatusEnum Status { get; set; }


		 public long? ReviewedByUserId { get; set; }

		 		 public Guid WholesaleEclId { get; set; }

		 
    }
}