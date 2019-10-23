using Abp.Application.Services.Dto;
using System;

namespace TestDemo.WholesaleAssumption.Dtos
{
    public class GetAllWholesaleEclPdSnPCummulativeDefaultRatesesInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }

		public string KeyFilter { get; set; }

		public string RatingFilter { get; set; }

		public int? MaxYearsFilter { get; set; }
		public int? MinYearsFilter { get; set; }

		public double? MaxValueFilter { get; set; }
		public double? MinValueFilter { get; set; }


		 public string WholesaleEclTenantIdFilter { get; set; }

		 
    }
}