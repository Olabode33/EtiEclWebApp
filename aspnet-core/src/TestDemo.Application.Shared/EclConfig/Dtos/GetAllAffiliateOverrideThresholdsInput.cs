using Abp.Application.Services.Dto;
using System;

namespace TestDemo.EclConfig.Dtos
{
    public class GetAllAffiliateOverrideThresholdsInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }


		 public string OrganizationUnitDisplayNameFilter { get; set; }

		 
    }
}