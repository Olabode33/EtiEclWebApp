
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.ObeAssumption.Dtos
{
    public class CreateOrEditObeEclPdAssumptionNonInternalModelDto : EntityDto<Guid?>
    {

		 public Guid ObeEclId { get; set; }
		 
		 
    }
}