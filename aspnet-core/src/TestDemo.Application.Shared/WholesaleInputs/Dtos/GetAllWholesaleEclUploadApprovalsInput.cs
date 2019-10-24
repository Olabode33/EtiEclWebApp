using Abp.Application.Services.Dto;
using System;

namespace TestDemo.WholesaleInputs.Dtos
{
    public class GetAllWholesaleEclUploadApprovalsInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }

		public DateTime? MaxReviewedDateFilter { get; set; }
		public DateTime? MinReviewedDateFilter { get; set; }

		public string ReviewCommentFilter { get; set; }

		public int StatusFilter { get; set; }


		 public string WholesaleEclUploadUploadCommentFilter { get; set; }

		 		 public string UserNameFilter { get; set; }

		 
    }
}