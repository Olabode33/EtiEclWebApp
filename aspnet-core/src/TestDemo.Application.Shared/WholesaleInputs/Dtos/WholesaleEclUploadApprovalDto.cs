using TestDemo.EclShared;

using System;
using Abp.Application.Services.Dto;

namespace TestDemo.WholesaleInputs.Dtos
{
    public class WholesaleEclUploadApprovalDto : EntityDto<Guid>
    {
		public DateTime? ReviewedDate { get; set; }

		public string ReviewComment { get; set; }

		public GeneralStatusEnum Status { get; set; }


		 public Guid WholesaleEclUploadId { get; set; }

		 		 public long? ReviewedByUserId { get; set; }

		 
    }
}