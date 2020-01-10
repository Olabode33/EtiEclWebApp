using System;
using Abp.Application.Services.Dto;
using TestDemo.Dto;

namespace TestDemo.InvestmentAssumption.Dtos
{
    public class CreateOrEditInvestmentEclEadInputAssumptionDto : CreateOrEditEclEadAssumptionBase
    {
		 public Guid? InvestmentEclId { get; set; }
    }
}