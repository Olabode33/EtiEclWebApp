using Abp.Application.Services.Dto;
using System;

namespace TestDemo.WholesaleResults.Dtos
{
    public class GetAllWholesaleEclResultSummariesInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }

		public int SummaryTypeFilter { get; set; }

		public string TitleFilter { get; set; }


		 public string WholesaleEclTenantIdFilter { get; set; }

		 
    }
}