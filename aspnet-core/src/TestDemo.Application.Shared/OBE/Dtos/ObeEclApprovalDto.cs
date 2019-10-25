using TestDemo.EclShared;

using System;
using Abp.Application.Services.Dto;

namespace TestDemo.OBE.Dtos
{
    public class ObeEclApprovalDto : EntityDto<Guid>
    {
		public DateTime? ReviewedDate { get; set; }

		public string ReviewComment { get; set; }

		public GeneralStatusEnum Status { get; set; }


		 public Guid? ObeEclId { get; set; }

		 		 public long? ReviewedByUserId { get; set; }

		 
    }
}