using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.WholesaleComputation.Dtos
{
    public class GetWholesaleLgdCollateralTypeDataForEditOutput
    {
		public CreateOrEditWholesaleLgdCollateralTypeDataDto WholesaleLgdCollateralTypeData { get; set; }

		public string WholesaleEclTenantId { get; set;}


    }
}