
using System;
using Abp.Application.Services.Dto;

namespace TestDemo.WholesaleComputation.Dtos
{
    public class WholesalePdMappingDto : EntityDto<Guid>
    {
		public string ContractId { get; set; }


		 public Guid? WholesaleEclId { get; set; }

		 
    }
}