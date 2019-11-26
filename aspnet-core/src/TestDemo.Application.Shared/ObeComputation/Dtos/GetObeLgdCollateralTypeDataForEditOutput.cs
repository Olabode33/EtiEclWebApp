using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.ObeComputation.Dtos
{
    public class GetObeLgdCollateralTypeDataForEditOutput
    {
		public CreateOrEditObeLgdCollateralTypeDataDto ObeLgdCollateralTypeData { get; set; }

		public string ObeEclTenantId { get; set;}


    }
}