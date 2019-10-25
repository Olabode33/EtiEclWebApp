using TestDemo.EclShared;

using System;
using Abp.Application.Services.Dto;

namespace TestDemo.Retail.Dtos
{
    public class RetailEclApprovalDto : EntityDto<Guid>
    {
		public DateTime? ReviewedDate { get; set; }

		public string ReviewComment { get; set; }

		public GeneralStatusEnum Status { get; set; }


		 public long? ReviewedByUserId { get; set; }

		 		 public Guid? RetailEclId { get; set; }

		 
    }
}