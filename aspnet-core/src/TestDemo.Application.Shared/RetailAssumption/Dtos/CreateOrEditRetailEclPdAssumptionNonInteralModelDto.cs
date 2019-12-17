
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.RetailAssumption.Dtos
{
    public class CreateOrEditRetailEclPdAssumptionNonInteralModelDto : EntityDto<Guid?>
    {

		 public Guid RetailEclId { get; set; }
		 
		 
    }
}