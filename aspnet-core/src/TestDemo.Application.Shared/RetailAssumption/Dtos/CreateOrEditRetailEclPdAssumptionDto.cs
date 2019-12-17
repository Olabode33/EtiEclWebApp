using TestDemo.EclShared;
using TestDemo.EclShared;
using TestDemo.EclShared;

using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.RetailAssumption.Dtos
{
    public class CreateOrEditRetailEclPdAssumptionDto : EntityDto<Guid?>
    {

		 public Guid RetailEclId { get; set; }
		 
		 
    }
}