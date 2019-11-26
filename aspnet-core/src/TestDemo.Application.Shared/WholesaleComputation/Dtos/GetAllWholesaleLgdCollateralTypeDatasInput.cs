using Abp.Application.Services.Dto;
using System;

namespace TestDemo.WholesaleComputation.Dtos
{
    public class GetAllWholesaleLgdCollateralTypeDatasInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }

		public double? MaxINVENTORY_OMVFilter { get; set; }
		public double? MinINVENTORY_OMVFilter { get; set; }


		 public string WholesaleEclTenantIdFilter { get; set; }

		 
    }
}