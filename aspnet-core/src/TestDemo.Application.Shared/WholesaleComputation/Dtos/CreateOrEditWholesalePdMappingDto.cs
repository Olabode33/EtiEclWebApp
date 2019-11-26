
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.WholesaleComputation.Dtos
{
    public class CreateOrEditWholesalePdMappingDto : EntityDto<Guid?>
    {

		public string ContractId { get; set; }
		
		
		 public Guid? WholesaleEclId { get; set; }
		 
		 
    }
}