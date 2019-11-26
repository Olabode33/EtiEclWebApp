
using System;
using Abp.Application.Services.Dto;

namespace TestDemo.WholesaleComputation.Dtos
{
    public class WholesaleLgdCollateralTypeDataDto : EntityDto<Guid>
    {
		public double INVENTORY_OMV { get; set; }


		 public Guid? WholesaleEclId { get; set; }

		 
    }
}