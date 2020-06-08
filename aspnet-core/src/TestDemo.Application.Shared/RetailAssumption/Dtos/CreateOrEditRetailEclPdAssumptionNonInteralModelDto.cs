
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;
using TestDemo.EclShared;
using TestDemo.Dto.Assumptions;

namespace TestDemo.RetailAssumption.Dtos
{
    public class CreateOrEditRetailEclPdAssumptionNonInteralModelDto : CreateOrEditEclPdAssumptionNonInteralModelDtoBase
    {
		 public Guid RetailEclId { get; set; }
    }
}