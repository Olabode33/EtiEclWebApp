using TestDemo.EclShared;

using System;
using Abp.Application.Services.Dto;

namespace TestDemo.RetailComputation.Dtos
{
    public class RetailEclSicrApprovalDto : EntityDto<Guid>
    {
		public DateTime? ReviewedDate { get; set; }

		public string ReviewComment { get; set; }

		public GeneralStatusEnum Status { get; set; }


		 public Guid? RetailEclSicrId { get; set; }

		 		 public long? ReviewedByUserId { get; set; }

		 
    }
}