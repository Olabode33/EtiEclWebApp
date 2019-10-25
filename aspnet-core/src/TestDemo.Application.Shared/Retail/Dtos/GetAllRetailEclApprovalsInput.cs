using Abp.Application.Services.Dto;
using System;

namespace TestDemo.Retail.Dtos
{
    public class GetAllRetailEclApprovalsInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }

		public DateTime? MaxReviewedDateFilter { get; set; }
		public DateTime? MinReviewedDateFilter { get; set; }

		public string ReviewCommentFilter { get; set; }

		public int StatusFilter { get; set; }


		 public string UserNameFilter { get; set; }

		 		 public string RetailEclTenantIdFilter { get; set; }

		 
    }
}