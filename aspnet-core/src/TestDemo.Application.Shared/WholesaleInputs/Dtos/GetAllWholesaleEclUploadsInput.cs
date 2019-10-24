using Abp.Application.Services.Dto;
using System;

namespace TestDemo.WholesaleInputs.Dtos
{
    public class GetAllWholesaleEclUploadsInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }

		public int DocTypeFilter { get; set; }

		public string UploadCommentFilter { get; set; }

		public int StatusFilter { get; set; }


		 public string WholesaleEclTenantIdFilter { get; set; }

		 
    }
}