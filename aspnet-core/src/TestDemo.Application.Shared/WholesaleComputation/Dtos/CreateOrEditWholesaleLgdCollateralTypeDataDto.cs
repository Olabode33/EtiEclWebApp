
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.WholesaleComputation.Dtos
{
    public class CreateOrEditWholesaleLgdCollateralTypeDataDto : EntityDto<Guid?>
    {

		public double INVENTORY_OMV { get; set; }
		
		
		 public Guid? WholesaleEclId { get; set; }
		 
		 
    }
}