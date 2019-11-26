
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.RetailComputation.Dtos
{
    public class CreateOrEditRetailPdLifetimeBestDto : EntityDto<Guid?>
    {

		 public Guid? RetailEclId { get; set; }
		 
		 
    }
}