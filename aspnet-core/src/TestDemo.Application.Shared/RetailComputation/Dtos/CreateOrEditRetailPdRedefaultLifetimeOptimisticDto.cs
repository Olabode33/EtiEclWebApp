
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.RetailComputation.Dtos
{
    public class CreateOrEditRetailPdRedefaultLifetimeOptimisticDto : EntityDto<Guid?>
    {

		 public Guid RetailEclId { get; set; }
		 
		 
    }
}