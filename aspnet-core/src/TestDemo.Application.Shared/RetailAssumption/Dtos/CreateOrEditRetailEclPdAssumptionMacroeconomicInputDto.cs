using TestDemo.EclShared;
using TestDemo.EclShared;

using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;
using TestDemo.Dto.Assumptions;

namespace TestDemo.RetailAssumption.Dtos
{
    public class CreateOrEditRetailEclPdAssumptionMacroeconomicInputDto: CreateOrEditEclPdAssumptionMacroeconomicInputDtoBase
    {
		 public Guid RetailEclId { get; set; }
    }
}