using Abp.Application.Services.Dto;
using System;

namespace TestDemo.RetailInputs.Dtos
{
    public class GetAllRetailEclUploadsInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }

		public int DocTypeFilter { get; set; }

		public string UploadCommentFilter { get; set; }

		public int StatusFilter { get; set; }


		 public string RetailEclTenantIdFilter { get; set; }

		 
    }
}