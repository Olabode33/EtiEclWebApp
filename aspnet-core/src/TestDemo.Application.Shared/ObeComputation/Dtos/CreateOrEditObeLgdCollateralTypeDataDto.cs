
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.ObeComputation.Dtos
{
    public class CreateOrEditObeLgdCollateralTypeDataDto : EntityDto<Guid?>
    {

		public double INVENTORY_OMV { get; set; }
		
		
		 public Guid? ObeEclId { get; set; }
		 
		 
    }
}