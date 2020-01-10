using TestDemo.EclShared;
using TestDemo.EclShared;

using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;
using TestDemo.Dto;

namespace TestDemo.RetailAssumption.Dtos
{
    public class CreateOrEditRetailEclLgdAssumptionDto : CreateOrEditEclLgdAssumptionBase
    {
		 public Guid? RetailEclId { get; set; }
    }
}