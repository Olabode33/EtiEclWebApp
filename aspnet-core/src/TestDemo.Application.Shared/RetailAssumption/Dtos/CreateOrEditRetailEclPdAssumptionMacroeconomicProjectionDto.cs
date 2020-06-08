using TestDemo.EclShared;
using TestDemo.EclShared;

using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;
using TestDemo.Dto.Assumptions;

namespace TestDemo.RetailAssumption.Dtos
{
    public class CreateOrEditRetailEclPdAssumptionMacroeconomicProjectionDto : CreateOrEditEclPdAssumptionMacroeconomicProjectionDtoBase
    {

		 public Guid RetailEclId { get; set; }
    }
}