using Abp.Application.Services.Dto;
using System;

namespace TestDemo.Wholesale.Dtos
{
    public class GetAllWholesaleEclApprovalsInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }

		public DateTime? MaxReviewedDateFilter { get; set; }
		public DateTime? MinReviewedDateFilter { get; set; }

		public int StatusFilter { get; set; }


		 public string UserNameFilter { get; set; }

		 		 public string WholesaleEclTenantIdFilter { get; set; }

		 
    }
}