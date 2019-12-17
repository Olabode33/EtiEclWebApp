using TestDemo.EclShared;
using TestDemo.EclShared;

using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.RetailAssumption.Dtos
{
    public class CreateOrEditRetailEclPdAssumptionMacroeconomicProjectionDto : EntityDto<Guid?>
    {

		 public Guid RetailEclId { get; set; }
		 
		 
    }
}