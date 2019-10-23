using Abp.Application.Services.Dto;
using System;

namespace TestDemo.Wholesale.Dtos
{
    public class GetAllWholesaleEclsInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }

		public DateTime? MaxReportingDateFilter { get; set; }
		public DateTime? MinReportingDateFilter { get; set; }

		public DateTime? MaxClosedDateFilter { get; set; }
		public DateTime? MinClosedDateFilter { get; set; }

		public int IsApprovedFilter { get; set; }

		public int StatusFilter { get; set; }


		 public string UserNameFilter { get; set; }

		 
    }
}