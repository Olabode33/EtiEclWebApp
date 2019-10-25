using Abp.Application.Services.Dto;
using System;

namespace TestDemo.ObeInputs.Dtos
{
    public class GetAllObeEclUploadApprovalsInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }

		public DateTime? MaxReviewedDateFilter { get; set; }
		public DateTime? MinReviewedDateFilter { get; set; }

		public string ReviewCommentFilter { get; set; }

		public int StatusFilter { get; set; }


		 public string ObeEclUploadTenantIdFilter { get; set; }

		 		 public string UserNameFilter { get; set; }

		 
    }
}