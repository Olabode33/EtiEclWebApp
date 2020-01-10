using TestDemo.EclShared;
using TestDemo.EclShared;
using TestDemo.EclShared;

using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;
using TestDemo.Dto;

namespace TestDemo.InvestmentAssumption.Dtos
{
    public class CreateOrEditInvestmentEclPdInputAssumptionDto : CreateOrEditEclPdAssumptionBase
    { 
		 public Guid InvestmentEclId { get; set; }
    }
}