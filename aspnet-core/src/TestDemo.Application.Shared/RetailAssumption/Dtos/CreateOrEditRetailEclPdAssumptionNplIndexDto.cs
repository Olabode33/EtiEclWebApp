﻿using TestDemo.EclShared;

using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;
using TestDemo.Dto.Assumptions;

namespace TestDemo.RetailAssumption.Dtos
{
    public class CreateOrEditRetailEclPdAssumptionNplIndexDto : CreateOrEditEclPdAssumptionNplIndexDtoBase
    {
		 public Guid RetailEclId { get; set; }
    }
}