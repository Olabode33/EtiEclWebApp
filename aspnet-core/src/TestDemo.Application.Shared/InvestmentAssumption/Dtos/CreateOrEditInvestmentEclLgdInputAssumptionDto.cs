using TestDemo.EclShared;
using TestDemo.EclShared;

using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;
using TestDemo.Dto;

namespace TestDemo.InvestmentAssumption.Dtos
{
    public class CreateOrEditInvestmentEclLgdInputAssumptionDto : CreateOrEditEclLgdAssumptionBase
    {	
		 public Guid InvestmentEclId { get; set; }	 
    }
}