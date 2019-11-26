
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.RetailComputation.Dtos
{
    public class CreateOrEditRetailLgdContractDataDto : EntityDto<Guid?>
    {

		public string CONTRACT_NO { get; set; }
		
		
		 public Guid RetailEclId { get; set; }
		 
		 
    }
}