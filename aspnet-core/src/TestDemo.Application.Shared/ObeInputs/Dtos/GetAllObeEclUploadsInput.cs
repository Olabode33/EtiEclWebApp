using Abp.Application.Services.Dto;
using System;

namespace TestDemo.ObeInputs.Dtos
{
    public class GetAllObeEclUploadsInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }

		public int DocTypeFilter { get; set; }

		public string UploadCommentFilter { get; set; }

		public int StatusFilter { get; set; }


		 public string ObeEclTenantIdFilter { get; set; }

		 
    }
}