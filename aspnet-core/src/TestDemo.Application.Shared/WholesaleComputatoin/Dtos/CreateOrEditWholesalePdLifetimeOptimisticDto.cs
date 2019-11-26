
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.WholesaleComputatoin.Dtos
{
    public class CreateOrEditWholesalePdLifetimeOptimisticDto : EntityDto<Guid?>
    {

		 public Guid? WholesaleEclId { get; set; }
		 
		 
    }
}