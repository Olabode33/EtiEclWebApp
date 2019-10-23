using Abp.Application.Services.Dto;
using System;

namespace TestDemo.WholesaleAssumption.Dtos
{
    public class GetAllWholesaleEclPdAssumption12MonthsesInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }

		public int? MaxCreditFilter { get; set; }
		public int? MinCreditFilter { get; set; }

		public double? MaxPDFilter { get; set; }
		public double? MinPDFilter { get; set; }

		public string SnPMappingEtiCreditPolicyFilter { get; set; }

		public string SnPMappingBestFitFilter { get; set; }


		 public string WholesaleEclTenantIdFilter { get; set; }

		 
    }
}