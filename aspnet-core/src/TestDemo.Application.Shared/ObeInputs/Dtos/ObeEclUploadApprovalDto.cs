using TestDemo.EclShared;

using System;
using Abp.Application.Services.Dto;

namespace TestDemo.ObeInputs.Dtos
{
    public class ObeEclUploadApprovalDto : EntityDto<Guid>
    {
		public DateTime? ReviewedDate { get; set; }

		public string ReviewComment { get; set; }

		public GeneralStatusEnum Status { get; set; }


		 public Guid? ObeEclUploadId { get; set; }

		 		 public long? ReviewedByUserId { get; set; }

		 
    }
}