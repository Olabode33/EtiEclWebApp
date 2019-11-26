
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.WholesaleComputation.Dtos
{
    public class CreateOrEditWholesalePdRedefaultLifetimeOptimisticDto : EntityDto<Guid?>
    {

		 public Guid? WholesaleEclId { get; set; }
		 
		 
    }
}